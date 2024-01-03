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

    private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

    public LogINModel(UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
    {
        _userManager = userManager;
    }
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

    public class IdentitySeedData
    {
        public static async Task Initialize(PoilsiavietesContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {

            context.Database.EnsureCreated();

            string asdminRole = "Admin";
            string memberRole = "Member";
            string password4all = "Password123#";

            if (await roleManager.FindByNameAsync(asdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(asdminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("aa@aa.aa") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "aa@aa.aa",
                    Email = "aa@aa.aa",
                    PhoneNumber = "6902341234"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, asdminRole);
                }
            }

            if (await userManager.FindByNameAsync("mm@mm.mm") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "mm@mm.mm",
                    Email = "mm@mm.mm",
                    PhoneNumber = "1112223333"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }

        internal static object Initialize(PoilsiavietesContext context, UserManager<IdentityUser> userMgr, RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roleMgr)
        {
            throw new NotImplementedException();
        }
    }
}
