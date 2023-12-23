using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class IndexModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public IndexModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IList<Poilsiaviete> Poilsiaviete { get; set; } = new List<Poilsiaviete>();
       
        public async Task OnGetAsync(DateOnly dateFrom, DateOnly dateTill)
        {
            if (dateFrom.Year != 1 && dateTill.Year != 1)
            {
                if (dateTill <= dateFrom)
                {
                    TempData["ErrorMessage"] = "Nurodytas laikotarpis neteisingas.";
                    return;
                }
                if (_context.Poilsiavietes != null)
                {
                    Poilsiaviete = await FilterPoilsiavietes(dateFrom, dateTill);
                    List<Tuple<double, double, string>> locations = new List<Tuple<double, double, string>>();

                    string apiKey = Environment.GetEnvironmentVariable("MAPS_API_KEY");
                    using HttpClient client = new HttpClient();
                    foreach (Poilsiaviete poilsiaviete in Poilsiaviete)
                    {
                        string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(poilsiaviete.Adresas)}&key={apiKey}";

                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(responseBody);
                            if (json.HasValues)
                            {
                                string status = (string)json["status"];
                                if (status == "OK")
                                {
                                    double platuma = (double)json["results"][0]["geometry"]["location"]["lat"];
                                    double ilguma = (double)json["results"][0]["geometry"]["location"]["lng"];

                                    locations.Add(new Tuple<double, double, string>(platuma, ilguma,
                                        $"Pavadinimas: {poilsiaviete.Pavadinimas}<br>Adresas: {poilsiaviete.Adresas}"));
                                }
                            }
                        }
                        var serializedLocations = JsonConvert.SerializeObject(locations);
                        TempData["SerializedLocations"] = serializedLocations;
                    }
                }
            }
        }
        public async Task<List<Poilsiaviete>> FilterPoilsiavietes(DateOnly dateFrom, DateOnly dateTill)
        {
            var Rezervacijos = await _context.Rezervacijos
                .Where(r => r.Busena < 3)
                .Where(r => !(r.Pabaiga < dateFrom || r.Pradzia > dateTill))
                .ToListAsync();

            var nefiltruotosPoilsiavietes = await _context.Poilsiavietes
                .Include(p => p.FkIdNaudotojasNavigation)
                .Include(p => p.FkKodasNavigation)
                .Include(p => p.TipasNavigation)
                .ToListAsync();

            var filtruotosPoilsiavietes = nefiltruotosPoilsiavietes
                .Where(p => !Rezervacijos.Any(r => r.FkIdPoilsiaviete == p.IdPoilsiaviete))
                .ToList();

            return filtruotosPoilsiavietes;
        }
    }
}