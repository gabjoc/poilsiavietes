using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class IndexModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public IndexModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IList<Poilsiaviete> Poilsiaviete { get;set; } = new List<Poilsiaviete>();

        public async Task OnGetAsync(DateOnly dateFrom, DateOnly dateTill)
        {
            if(dateFrom.Year != 1 &&  dateTill.Year != 1)
            {
                if (_context.Poilsiavietes != null)
                {
                    Poilsiaviete = await FilterPoilsiavietes(dateFrom, dateTill);
                }
            }
        }

        private async Task<List<Poilsiaviete>> FilterPoilsiavietes(DateOnly dateFrom, DateOnly dateTill)
        {
            var Rezervacijos = await _context.Rezervacijos
                .Where(r => r.Busena < 3)
                .Where(r => !(r.Pabaiga < dateFrom || r.Pradzia > dateTill))
                .ToListAsync();

            var nefiltruotosPoilsiavietes = await _context.Poilsiavietes
                .Include(p => p.FkIdNaudotojasNavigation)
                .Include(p => p.FkKodasNavigation)
                .Include(p => p.TipasNavigation)
                .ToListAsync();

            var filtruotosPoilsiavietes = nefiltruotosPoilsiavietes
                .Where(p => !Rezervacijos.Any(r => r.FkIdPoilsiaviete == p.IdPoilsiaviete))
                .ToList();

            return filtruotosPoilsiavietes;
        }
    }
}
