using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

public class LogOUTModel : PageModel
{
    public IActionResult OnGet()
    {

        // Redirect to the home page after logout
        return RedirectToPage("/Index");
    }
}
