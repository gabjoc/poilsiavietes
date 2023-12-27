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
        public Rezervacija redaguojamaRezervacija { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rezervacijos == null)
            {
                return NotFound();
            }

            var rezervacija =  await _context.Rezervacijos.FirstOrDefaultAsync(m => m.Numeris == id);
            if (rezervacija == null)
            {
                return NotFound();
            }
        redaguojamaRezervacija = rezervacija;
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

            _context.Attach(redaguojamaRezervacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijosExists(redaguojamaRezervacija.Numeris))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RezervacijosExists(int id)
        {
          return (_context.Rezervacijos?.Any(e => e.Numeris == id)).GetValueOrDefault();
        }
    }
}
