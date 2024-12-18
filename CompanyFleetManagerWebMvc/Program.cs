using CompanyFleetManager;
using CompanyFleetManagerWebApp;
using CompanyFleetManagerWebApp.Models;
using CompanyFleetManagerWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CompanyFleetManagerWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add and configure web services
            builder.Services.AddScoped<WebServiceFleetApi>();
            builder.Services.AddHttpClient<WebServiceFleetApi>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:52819/api/fleet/");
            });

            builder.Services.AddScoped<WebServiceAuthenticationApi>();
            builder.Services.AddHttpClient<WebServiceAuthenticationApi>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:52819/api/users/");
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add Razor pages service
            builder.Services.AddRazorPages();

            //login state info singleton
            builder.Services.AddSingleton<UserLoggedState>();

            //build the web app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            app.UseRouting();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
