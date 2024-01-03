using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Poilsiavietes.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Microsoft.AspNet.Identity.EntityFramework.IdentityUser;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

public class LogINModel : PageModel
{

    private readonly PoilsiavietesContext _dbContext;
    private readonly ILogger<LogINModel> _logger;

    public LogINModel(PoilsiavietesContext dbContext, ILogger<LogINModel> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            var user = _dbContext.Naudotojais.FirstOrDefault(u => u.VartotojoVardas == Input.UserName && u.Slaptazodis == Input.Password);
            if (user != null)
            {
                // Successful login logic
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return Page();
    }

}
