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
    public class ReadModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public ReadModel(PoilsiavietesContext context)
        {
            _context = context;
        }

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
    }
}
