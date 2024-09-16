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
        public static string DatabaseFilename { get; set; } = "users.db";
        public static string ConnectionString { get; set; } = $"Data Source={DatabaseFilename}";

        public UsersDatabaseContext() { }

        public UsersDatabaseContext(DbContextOptions<UsersDatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConnectionString);
            }
        }
    }
}
