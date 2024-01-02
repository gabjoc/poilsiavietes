using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Profilis
{
    public class DeleteModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public DeleteModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Naudotojai Naudotojai { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Naudotojais == null)
            {
                return NotFound();
            }

            var naudotojai = await _context.Naudotojais.FirstOrDefaultAsync(m => m.IdNaudotojas == id);

            if (naudotojai == null)
            {
                return NotFound();
            }
            else 
            {
                Naudotojai = naudotojai;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Naudotojais == null)
            {
                return NotFound();
            }
            var naudotojai = await _context.Naudotojais.FindAsync(id);

            if (naudotojai != null)
            {
                Naudotojai = naudotojai;
                _context.Naudotojais.Remove(Naudotojai);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
