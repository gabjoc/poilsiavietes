using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class EditModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public EditModel(PoilsiavietesContext context)
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
            var poilsiaviete =  await _context.Poilsiavietes.FirstOrDefaultAsync(m => m.IdPoilsiaviete == id);
            if (poilsiaviete == null)
            {
                return NotFound();
            }
            Poilsiaviete = poilsiaviete;
           ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
           ViewData["FkKodas"] = new SelectList(_context.Miestais, "Kodas", "Kodas");
           ViewData["Tipas"] = new SelectList(_context.Tipais, "IdTipas", "Name");
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

            _context.Attach(Poilsiaviete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoilsiavieteExists(Poilsiaviete.IdPoilsiaviete))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Message"] = "Duomenys issaugoti sekmingai.";
            var routeValues = new RouteValueDictionary(RouteData.Values);
            foreach (var queryParameter in HttpContext.Request.Query)
            {
                routeValues[queryParameter.Key] = queryParameter.Value.ToString();
            }
            return RedirectToPage(routeValues);
        }

        private bool PoilsiavieteExists(int id)
        {
          return (_context.Poilsiavietes?.Any(e => e.IdPoilsiaviete == id)).GetValueOrDefault();
        }
    }
}
