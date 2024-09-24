using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager
{
    public class UsersDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public static string ServerAddress { get; set; } = $"DESKTOP-1B6DSC3\\SQLEXPRESS";
        public static string DatabaseName { get; set; } = "Users";
        public static string ConnectionString { get; set; } = $"Server={ServerAddress};Database={DatabaseName};Trusted_Connection=True;Encrypt=False";

        public UsersDatabaseContext() { }

        public UsersDatabaseContext(DbContextOptions<UsersDatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
