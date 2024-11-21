using Microsoft.AspNetCore.Identity;

namespace CompanyFleetManager
{
    public class Utils
    {
        public static async Task CreateRoles(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateAdminUserTask(userManager, roleManager);
        }

        private static async Task CreateAdminUserTask(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Console.WriteLine("Creating admin account!");

            var adminEmail = Environment.GetEnvironmentVariable("CFM_ADMIN_EMAIL");
            var adminPassword = Environment.GetEnvironmentVariable("CFM_ADMIN_PASSWORD");

            if (string.IsNullOrWhiteSpace(adminEmail))
            {
                throw new Exception("CFM_ADMIN_EMAIL env. variable is not set! Configuration is required.");
            }

            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new Exception("CFM_ADMIN_PASSWORD env. variable is not set! Configuration is required.");
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
