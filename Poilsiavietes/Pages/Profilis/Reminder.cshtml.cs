using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;
using System.Linq;

public class ReminderModel : PageModel
{
    private readonly PoilsiavietesContext _context;

    public ReminderModel(PoilsiavietesContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Email { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Check if the email exists in the database
        var user = _context.Naudotojais.FirstOrDefault(u => u.ElPastas == Email);

        if (user == null)
        {
            // If the email does not exist, show an error message
            ModelState.AddModelError(string.Empty, "No user found with this email.");
            return Page();
        }

        // TODO: Send an email to the user with their username

        // For demonstration purposes, let's assume the email has been sent successfully
        ViewData["SuccessMessage"] = $"Username reminder sent to {Email}";

        return Page();
    }
}
