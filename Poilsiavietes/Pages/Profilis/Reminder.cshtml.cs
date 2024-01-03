using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;


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

        try
        {
            string smtpServer = "server.com";
            int smtpPort = 587;
            string smtpUsername = "admin";
            string smtpPassword = "password";

            // Create a MailMessage object
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("your-email@example.com"); // Set your sender email address
            mailMessage.To.Add(Email);
            mailMessage.Subject = "Username Reminder";
            mailMessage.Body = $"Your username is: {user.VartotojoVardas}";

            // Create a SmtpClient object
            var smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            // Send the email
            smtpClient.Send(mailMessage);

            // Display success message
            ViewData["SuccessMessage"] = $"Username reminder sent to {Email}";
        }
        catch (Exception ex)
        {
            // Handle exceptions related to email sending (e.g., SMTP configuration issues)
            ModelState.AddModelError(string.Empty, "Failed to send the username reminder. Please try again later.");
        }

        return Page();
    }
}
