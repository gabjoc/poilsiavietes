using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class CreateModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public CreateModel(PoilsiavietesContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Rezervacija kuriamRezervacija { get; set; }

        public IActionResult OnGetRezervuoti()
        {
            kuriamRezervacija = new Rezervacija();
            kuriamRezervacija.SukurimoData = DateOnly.FromDateTime(DateTime.Today);
            //kuriamRezervacija.Apmoketa = false;
            kuriamRezervacija.Busena = 1;

            ViewData["Busena"] = new SelectList(_context.Busenos, "IdBusena", "IdBusena");
            ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
            ViewData["FkIdPoilsiaviete"] = new SelectList(_context.Poilsiavietes, "IdPoilsiaviete", "IdPoilsiaviete");

            return Page();
        }
        /*
        public IActionResult OnGetRezervuoti()
        {
        ViewData["Busena"] = new SelectList(_context.Busenos, "IdBusena", "IdBusena");
        ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
        ViewData["FkIdPoilsiaviete"] = new SelectList(_context.Poilsiavietes, "IdPoilsiaviete", "IdPoilsiaviete");
            // Check if kuriamaRezervacija is null and initialize only if it's null
            if (kuriamaRezervacija == null)
            {
                kuriamaRezervacija = new Rezervacija(); // Instantiate your Rezervacija model here
            }

            // Set the creation date to the current date if it's not set yet
            if (kuriamaRezervacija.SukurimoData == default)
            {
                kuriamaRezervacija.SukurimoData = DateOnly.FromDateTime(DateTime.Now.Date); // Use DateTime.UtcNow.Date if timezone-agnostic date is preferred
            }

            return Page();
        }

        [BindProperty]
        public Rezervacija kuriamaRezervacija { get; set; }*/
        /*
        return Page();
    }

    [BindProperty]
    public Rezervacija kuriamaRezervacija { get; set; } = default!;
    */

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rezervacijos == null || kuriamRezervacija == null)
            {
                TempData["Message"] = "Duomenys neteisingi.";
                return Page();
            }

          _context.Rezervacijos.Add(kuriamRezervacija);
          await _context.SaveChangesAsync();

          return RedirectToPage("./Index");
        }
    }
}
