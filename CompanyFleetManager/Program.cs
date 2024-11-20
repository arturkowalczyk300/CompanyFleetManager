using Microsoft.AspNetCore.Identity;

namespace CompanyFleetManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            var app = builder.Build();

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
