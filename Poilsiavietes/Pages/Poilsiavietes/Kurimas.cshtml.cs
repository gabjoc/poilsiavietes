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
            ViewData["FkKodas"] = new SelectList(_context.Miestais, "Kodas", "Pavadinimas");
            ViewData["Tipas"] = new SelectList(_context.Tipais, "IdTipas", "Name");
            ViewData["PoilsiavieciuPatogumai"] = new SelectList(_context.Patogumais, "IdPatogumas", "Pavadinimas");
//          ViewData["AutomobiliuStovejimoAikstele"] = new SelectList(_context.AutomobiliuAiksteliuSavininkais, "IdAutomobiliuAikstelesSavininkas", "Vardas");
            return Page();
        }

        [BindProperty]
        public Poilsiaviete Poilsiaviete { get; set; } = default!;

        public List<string> SelectedPatogumas { get; set; } = new List<string>();
        public List<int> PatogumasCount { get; set; } = new List<int>();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(List<int> SelectedPatogumas, List<int> PatogumasCount)
        {
          if (!ModelState.IsValid || _context.Poilsiavietes == null || Poilsiaviete == null)
            {
                TempData["Message"] = "Duomenys neteisingi.";
                var routeValues = new RouteValueDictionary(RouteData.Values);
                foreach (var queryParameter in HttpContext.Request.Query)
                {
                    routeValues[queryParameter.Key] = queryParameter.Value.ToString();
                }
                return RedirectToPage(routeValues);
            }
          
            Poilsiaviete.FkIdNaudotojas = 1;
            _context.Poilsiavietes.Add(Poilsiaviete);
            await _context.SaveChangesAsync();

            Poilsiaviete sukurtapoilsiaviete = _context.Poilsiavietes.OrderBy(p => p.IdPoilsiaviete).Last();
            foreach (int patogumoid in SelectedPatogumas)
            {
                int count = PatogumasCount[patogumoid - 1];
                PoilsiavieciuPatogumai patogumas = new PoilsiavieciuPatogumai();
                patogumas.Kiekis = count;
                patogumas.FkIdPoilsiaviete = sukurtapoilsiaviete.IdPoilsiaviete;
                patogumas.FkIdPatogumas = patogumoid;

                _context.PoilsiavieciuPatogumais.Add(patogumas);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
