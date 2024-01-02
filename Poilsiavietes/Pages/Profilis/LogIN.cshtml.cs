using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Poilsiavietes.Models;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class LogINModel : PageModel
{
    [BindProperty]
    public Credential Credential { get; set; }

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid) return RedirectToPage("/Index");

        //Verify the credential
        if(Credential.UserName =="sun" && Credential.Password=="shit")
        {
            //Creating security context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@gmail.com")
            };
            var identity = new ClaimsIdentity(claims, "cookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("cookie", claimsPrincipal);

            return RedirectToPage("/");
        }
        return Page();
    }

   
}
public class Credential
{
    [Required(ErrorMessage = "Username is required.")]
    [Display(Name = "Vartotojo vardas")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{9,}$", ErrorMessage = "The password must have at least one upper case letter, one number, one symbol, and be at least 9 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
