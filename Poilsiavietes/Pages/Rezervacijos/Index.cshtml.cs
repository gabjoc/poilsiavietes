using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;

namespace Poilsiavietes.Pages.Rezervacijos
{
    public class IndexModel : PageModel
    {
        private readonly PoilsiavietesContext _context;

        public IndexModel(PoilsiavietesContext context)
        {
            _context = context;
        }

        public IList<Rezervacija> Rezervacijos { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rezervacijos != null)
            {
                Rezervacijos = await _context.Rezervacijos
                .Include(r => r.BusenaNavigation)
                .Include(r => r.FkIdNaudotojasNavigation)
                .Include(r => r.FkIdPoilsiavieteNavigation).ToListAsync();
            }
        }
    }
}
