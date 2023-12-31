using Microsoft.EntityFrameworkCore;
using Poilsiavietes.Models;


namespace Poilsiavietes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var connectionString = "server=localhost;user=root;password=;database=poilsiavietes";
            var serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));

            builder.Services.AddDbContext<PoilsiavietesContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            //builder.Services.AddAuthentication().AddCookie("cookie", options =>
            //{
            //    options.Cookie.Name = "cookie";
            //    options.LoginPath = "/LogIN";
            //    options.LogoutPath = "/LogOUT";
            //});

            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}