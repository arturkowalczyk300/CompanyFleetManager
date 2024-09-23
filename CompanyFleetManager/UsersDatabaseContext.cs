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
        public static string ServerAddress { get; set; } = $"mssqlserver,1433";
        public static string DatabaseName { get; set; } = "Users";
        public static string ConnectionString { get; set; } = $"Server={ServerAddress};Database={DatabaseName};User Id=sa;Password=PwD1ADM#;TrustServerCertificate=True";

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
