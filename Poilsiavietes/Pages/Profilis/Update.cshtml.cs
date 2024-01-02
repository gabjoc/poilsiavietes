using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Profilis
{
    public class UpdateModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public UpdateModel(PoilsiavietesContext context)
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

            var naudotojai =  await _context.Naudotojais.FirstOrDefaultAsync(m => m.IdNaudotojas == id);
            if (naudotojai == null)
            {
                return NotFound();
            }
            Naudotojai = naudotojai;
           ViewData["NaudotojoTipas"] = new SelectList(_context.NaudotojuTipais, "IdNaudojoTipas", "IdNaudojoTipas");
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

            _context.Attach(Naudotojai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaudotojaiExists(Naudotojai.IdNaudotojas))
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

        private bool NaudotojaiExists(int id)
        {
          return (_context.Naudotojais?.Any(e => e.IdNaudotojas == id)).GetValueOrDefault();
        }
    }
}
