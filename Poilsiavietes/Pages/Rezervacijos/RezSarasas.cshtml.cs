using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class IndexModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public IndexModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IList<Rezervacija> Rezervacijos { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? Filter { get; set; }
        public SelectList BusenuSarasas { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Rezervacijos != null)
            {
                Rezervacijos = await FiltruotiRezervacijas(Filter);
            }
            var busenos = await _context.Busenos.ToListAsync();
            var pervadBusenos = busenos.Select(busena => new SelectListItem
            {
                Value = busena.IdBusena.ToString(),
                Text = PervadintiBusenas(busena.IdBusena)
            }).ToList();
            BusenuSarasas = new SelectList(pervadBusenos, "Value", "Text");
        }

        private string PervadintiBusenas(int id)
        {
            return id switch
            {
                1 => "Gauta",
                2 => "Patvirtinta",
                3 => "Atšaukta",
                4 => "Įvykdyta",
                _ => "Klaida"
            };
        }

        private async Task<List<Rezervacija>> FiltruotiRezervacijas(int? busena = null)
        {
            var Rezervacijos = await _context.Rezervacijos
                .Include(r => r.FkIdNaudotojasNavigation)
                .ToListAsync();

            if (busena.HasValue)
            {
                Rezervacijos = Rezervacijos
                    .Where(r => r.Busena == busena)
                    .ToList();
            }

            return Rezervacijos;
        }
    }
}
