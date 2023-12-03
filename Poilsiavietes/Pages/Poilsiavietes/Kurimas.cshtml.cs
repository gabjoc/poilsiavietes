using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Poilsiavietes
{
    public class CreateModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public CreateModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
        ViewData["FkKodas"] = new SelectList(_context.Miestais, "Kodas", "Kodas");
        ViewData["Tipas"] = new SelectList(_context.Tipais, "IdTipas", "Name");
            return Page();
        }

        [BindProperty]
        public Poilsiaviete Poilsiaviete { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Poilsiavietes == null || Poilsiaviete == null)
            {
                return Page();
            }

            _context.Poilsiavietes.Add(Poilsiaviete);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
