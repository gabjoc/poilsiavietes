﻿using System;
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

        public IList<Poilsiaviete> Poilsiaviete { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Poilsiavietes != null)
            {
                Poilsiaviete = await _context.Poilsiavietes
                .Include(p => p.FkIdNaudotojasNavigation)
                .Include(p => p.FkKodasNavigation)
                .Include(p => p.TipasNavigation).ToListAsync();
            }
        }
    }
}