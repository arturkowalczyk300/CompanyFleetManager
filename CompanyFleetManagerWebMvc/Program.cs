using CompanyFleetManager;
using CompanyFleetManagerWebApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CompanyFleetManagerWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register DBContext (fleet database)
            builder.Services.AddDbContext<FleetDatabaseContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("FleetConnection"));
            });

            //Register DBContext (users identities database)
            builder.Services.AddDbContext<UsersDatabaseContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("UsersConnection"));
            });

            //add identity service
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;

                //prevent brute force attacks
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.AllowedForNewUsers = false;
            })
                .AddEntityFrameworkStores<UsersDatabaseContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add Razor pages service
            builder.Services.AddRazorPages();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
