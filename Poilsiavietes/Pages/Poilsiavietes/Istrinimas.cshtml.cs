using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class DeleteModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public DeleteModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Poilsiaviete Poilsiaviete { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, DateOnly dateFrom, DateOnly dateTill)
        {
            if (id == null || _context.Poilsiavietes == null)
            {
                return NotFound();
            }
            TempData["DateFrom"] = dateFrom.ToString();
            TempData["DateTill"] = dateTill.ToString();
            var poilsiaviete = await _context.Poilsiavietes.FirstOrDefaultAsync(m => m.IdPoilsiaviete == id);

            if (poilsiaviete == null)
            {
                return NotFound();
            }
            else 
            {
                Poilsiaviete = poilsiaviete;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, DateOnly dateFrom, DateOnly dateTill)
        {
            if (id == null || _context.Poilsiavietes == null)
            {
                return NotFound();
            }
            var poilsiaviete = await _context.Poilsiavietes.FindAsync(id);

            if (poilsiaviete != null)
            {
                Poilsiaviete = poilsiaviete;
                _context.Poilsiavietes.Remove(Poilsiaviete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new {dateFrom=dateFrom, dateTill=dateTill});
        }
    }
}
