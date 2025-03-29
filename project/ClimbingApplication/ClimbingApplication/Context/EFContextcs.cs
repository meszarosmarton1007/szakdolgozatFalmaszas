using ClimbingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimbingApplication.Context
{
    public class EFContextcs : DbContext
    {
        public DbSet<Felhasznalok> Felhasznalok {  get; set; }

        public DbSet<Falak> Falak { get; set; }

        public DbSet<FalmaszoHelyek> FalmaszoHelyek { get; set; }

        public DbSet<Hozzaszolasok> Hozzaszolasok { get; set; }

        public DbSet<Utak> Utak { get; set; }

        public DbSet<Valaszok> Valaszok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\db\\database.db");
        }
    }
}
