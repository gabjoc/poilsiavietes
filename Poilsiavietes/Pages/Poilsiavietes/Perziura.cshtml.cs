using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class DetailsModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public DetailsModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public Poilsiaviete Poilsiaviete { get; set; } = default!;
        public AutomobiliuStovejimoAikstele Automobiliai { get; set; } = default!;
        public List<Kategorijo> Kategorijos { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id, DateOnly dateFrom, DateOnly dateTill)
        {
            if (id == null || _context.Poilsiavietes == null)
            {
                return NotFound();
            }
            TempData["DateFrom"] = dateFrom.ToString();
            TempData["DateTill"] = dateTill.ToString();
            var poilsiaviete = await _context.Poilsiavietes.Include(t => t.TipasNavigation)
                .Include(m => m.FkKodasNavigation).Include(p => p.PoilsiavieciuPatogumai)
                .ThenInclude(p => p.FkIdPatogumasNavigation)
                .ThenInclude(k => k.FkIdKategorijaNavigation)
                .Include(a => a.AutomobiliuStovejimoAiksteles)
                .ThenInclude(s => s.FkIdAutomobiliuAikstelesSavininkasNavigation).FirstOrDefaultAsync(m => m.IdPoilsiaviete == id);
            var kategorijos = await _context.Kategorijos.ToListAsync();
            if (poilsiaviete == null)
            {
                return NotFound();
            }
            else
            {
                Poilsiaviete = poilsiaviete;

                Kategorijos = kategorijos;
            }
            return Page();
        }

        public IActionResult OnGetRezervuoti()
        {
            bool prisNaudotojas = true;
            if (prisNaudotojas == true)
            {
                return RedirectToPage("../Rezervacijos/RezKurimas");   
            }
            else
            {
                return RedirectToPage("../Profilis/Register");
            }
        }
    }
}
