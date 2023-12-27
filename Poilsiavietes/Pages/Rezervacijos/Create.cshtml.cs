using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
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
        ViewData["Busena"] = new SelectList(_context.Busenos, "IdBusena", "IdBusena");
        ViewData["FkIdNaudotojas"] = new SelectList(_context.Naudotojais, "IdNaudotojas", "IdNaudotojas");
        ViewData["FkIdPoilsiaviete"] = new SelectList(_context.Poilsiavietes, "IdPoilsiaviete", "IdPoilsiaviete");
            return Page();
        }

        [BindProperty]
        public Rezervacija kuriamaRezervacija { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rezervacijos == null || kuriamaRezervacija == null)
            {
                return Page();
            }

            _context.Rezervacijos.Add(kuriamaRezervacija);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
