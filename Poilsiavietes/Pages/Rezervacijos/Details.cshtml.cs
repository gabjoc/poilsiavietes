using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class DetailsModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public DetailsModel(PoilsiavietesContext context)
        {
            _context = context;
        }

      public Rezervacija Rezervacijos { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rezervacijos == null)
            {
                return NotFound();
            }

            var rezervacijos = await _context.Rezervacijos.FirstOrDefaultAsync(m => m.Numeris == id);
            if (rezervacijos == null)
            {
                return NotFound();
            }
            else 
            {
                Rezervacijos = rezervacijos;
            }
            return Page();
        }
    }
}
