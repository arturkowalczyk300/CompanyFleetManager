using CompanyFleetManager;
using CompanyFleetManagerWebApp;
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

            //Register DBContext (fleet database)
            var fleetConnectionString = builder.Configuration.GetConnectionString("FleetConnection");
            if (fleetConnectionString == null || fleetConnectionString.Equals(""))
                fleetConnectionString = Environment.GetEnvironmentVariable("fleet_connection_string");

            builder.Services.AddDbContext<FleetDatabaseContext>(options =>
            {
                options.UseSqlServer(fleetConnectionString);
            });

            //Register DBContext (users identities database)
            var usersConnectionString = builder.Configuration.GetConnectionString("UsersConnection");
            if (usersConnectionString == null || usersConnectionString.Equals(""))
                usersConnectionString = Environment.GetEnvironmentVariable("users_connection_string");

            builder.Services.AddDbContext<UsersDatabaseContext>(options =>
            {
                options.UseSqlServer(usersConnectionString);
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

            //automatic logout
            builder.Services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                });

            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add Razor pages service
            builder.Services.AddRazorPages();

            var app = builder.Build();

            //add admin account if not already created
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                Utils.CreateRoles(
                    services.GetRequiredService<UserManager<IdentityUser>>(),
                    services.GetRequiredService<RoleManager<IdentityRole>>()
                    ).Wait();
            }

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
