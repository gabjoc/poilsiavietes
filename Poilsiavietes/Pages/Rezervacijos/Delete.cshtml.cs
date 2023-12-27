using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class DeleteModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public DeleteModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Rezervacija Rezervacija { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rezervacijos == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacijos.FirstOrDefaultAsync(m => m.Numeris == id);

            if (rezervacija == null)
            {
                return NotFound();
            }
            else 
            {
                Rezervacija = rezervacija;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Rezervacijos == null)
            {
                return NotFound();
            }
            var rezervacija = await _context.Rezervacijos.FindAsync(id);

            if (rezervacija != null)
            {
                Rezervacija = rezervacija;
                _context.Rezervacijos.Remove(Rezervacija);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
