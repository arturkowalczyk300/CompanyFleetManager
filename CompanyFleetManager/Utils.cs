using Microsoft.AspNetCore.Identity;

namespace CompanyFleetManager
{
    public class Utils
    {
        public static async Task CreateRoles(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateAdminUserTask(userManager, roleManager);
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        private static async Task CreateAdminUserTask(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Console.WriteLine("Creating admin account!");

            var adminEmail = "admin@fleet.com";
            var adminPassword = GetConfiguration()["AdminSettings:AdminPassword"];

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
