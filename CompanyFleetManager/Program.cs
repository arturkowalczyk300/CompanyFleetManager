using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CompanyFleetManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add environment variables
            builder.Configuration.AddEnvironmentVariables();

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

            builder.Services.AddControllers();

            //api controllers and database
            builder.Services.AddDbContext<FleetDatabaseContext>();
            builder.Services.AddScoped<FleetDatabaseAccess>();

            builder.Services.AddDbContext<UsersDatabaseContext>();
            builder.Services.AddScoped<UsersDatabaseAccess>();

            var app = builder.Build();

            // add admin account if not already created
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                Utils.CreateRoles(
                    services.GetRequiredService<UserManager<IdentityUser>>(),
                    services.GetRequiredService<RoleManager<IdentityRole>>()
                    ).Wait();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed errors in development
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map API routes to controllers
            });

            // Start the application
            app.Run();
        }
    }
}
