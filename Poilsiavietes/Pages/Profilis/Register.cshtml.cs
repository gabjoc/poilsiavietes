using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Profilis
{
    public class RegisterModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public RegisterModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["NaudotojoTipas"] = new SelectList(_context.NaudotojuTipais, "IdNaudojoTipas", "IdNaudojoTipas");
            return Page();
        }

        [BindProperty]
        public Naudotojai Naudotojai { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Naudotojais == null || Naudotojai == null)
            {
                ModelState.AddModelError(string.Empty, "Internal error occurred. Please try again later.");
                return Page();
            }

            // Set NaudotojasTipas to a fixed value which means registered user
            Naudotojai.NaudotojoTipas = 1;

            Naudotojai.IdNaudotojas = GenerateRandomId();
            _context.Naudotojais.Add(Naudotojai);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private int GenerateRandomId()
        {
            // Using a simple Random number generator for demonstration purposes
            // This may not guarantee uniqueness, especially with a large number of users
            Random random = new Random();
            return random.Next(1, 1000001);
        }
    }
}
