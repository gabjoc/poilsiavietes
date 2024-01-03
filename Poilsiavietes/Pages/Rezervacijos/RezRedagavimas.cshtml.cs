using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class EditModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public EditModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rezervacija redRezervacija { get; set; } = default!;
        public string CurrentDate { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            CurrentDate = DateTime.Now.ToString("dd-MM-yyyy");
            if (id == null || _context.Rezervacijos == null)
            {
                return NotFound();
            }

            var rezervacija =  await _context.Rezervacijos.FirstOrDefaultAsync(m => m.RezNumeris == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            //if (rezervacija.BusenaNavigation != null && 
            //    (rezervacija.BusenaNavigation.IdBusena != 1 && rezervacija.BusenaNavigation.IdBusena != 2))
            //{
            //    TempData["ErrorMessage"] = "Negalite redaguoti sios rezervacijos, nes ji atsaukta arba ivykdyta.";
            //}

            redRezervacija = rezervacija;
            //redRezervacija.Pradzia = rezervacija.Pradzia;
            //redRezervacija.Pabaiga = rezervacija.Pabaiga;

            ViewData["Busena"] = new SelectList(_context.Busenos, "IdBusena", "IdBusena");
            ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
            ViewData["FkIdPoilsiaviete"] = new SelectList(_context.Poilsiavietes, "IdPoilsiaviete", "IdPoilsiaviete");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            redRezervacija.SukurimoData = DateOnly.FromDateTime(DateTime.Today);
            _context.Attach(redRezervacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijosExists(redRezervacija.RezNumeris))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Message"] = "Duomenys issaugoti sekmingai.";
            var routeValues = new RouteValueDictionary(RouteData.Values);
            foreach (var queryParameter in HttpContext.Request.Query)
            {
                routeValues[queryParameter.Key] = queryParameter.Value.ToString();
            }
            return RedirectToPage(routeValues);
            //return RedirectToPage("./RezPerziura"); // neissipildziusios svajones
        }

        private bool RezervacijosExists(int id)
        {
          return (_context.Rezervacijos?.Any(e => e.RezNumeris == id)).GetValueOrDefault();
        }
    }
}
