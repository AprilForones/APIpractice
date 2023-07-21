using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.LocalDB
{
    public class MobileDB : DbContext
    {
        public MobileDB(DbContextOptions<MobileDB> options) : base(options)
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myapp.db"); var connectionString = $"Data Source={path}";

            string databaseFile = connectionString; if (File.Exists(databaseFile))
            {

                File.Delete(databaseFile);

            }


            Database.EnsureCreated();

            Database.Migrate();
        }
        public DbSet<MyLocation> MyLocations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myapp.db");

            var connectionString = $"Data Source={path}";

            optionsBuilder.UseSqlite(connectionString);

        }
    }
}
