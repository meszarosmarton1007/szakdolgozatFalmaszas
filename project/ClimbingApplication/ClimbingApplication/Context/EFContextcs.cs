using ClimbingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimbingApplication.Context
{
    public class EFContextcs : DbContext
    {
       public EFContextcs(DbContextOptions<EFContextcs> options) :  base(options){ }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Felhasznalok>()
                .HasIndex(f => f.felhasznaloNev)
                .IsUnique();

            //
            modelBuilder.Entity<FalmaszoHelyek>()
               .HasOne(f => f.Hozzaado)
               .WithMany()
               .HasForeignKey(f => f.FelhasznalokID)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FalmaszoHelyek>()
                .HasOne(f => f.Hozzaado)
                .WithMany()
                .HasForeignKey(f => f.FelhasznalokID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Falak>()
                .HasOne(f => f.Letrehozo)
                .WithMany()
                .HasForeignKey(f => f.FelhasznaloID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Utak>()
                .HasOne(u => u.UtLetrehozo)
                .WithMany()
                .HasForeignKey(u => u.FelhasznaloID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hozzaszolasok>()
                .HasOne(h => h.UtHozzaszolo)
                .WithMany()
                .HasForeignKey(h => h.FelhasznaloID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Valaszok>()
                .HasOne(v => v.Valasziro)
                .WithMany()
                .HasForeignKey(v => v.FelhasznaloID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Falak>()
               .HasOne(f => f.Falhelye)
               .WithMany(fh => fh.Falak)
               .HasForeignKey(f => f.FalmaszohelyID)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Utak>()
                .HasOne(u => u.Falonut)
                .WithMany()
                .HasForeignKey(u => u.FalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hozzaszolasok>()
                .HasOne(h => h.UtHozzaszolas)
                .WithMany(u => u.Hozzaszolasoks)
                .HasForeignKey(h => h.UtakID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Valaszok>()
                .HasOne(v => v.Valasz)
                .WithMany(h => h.Valaszok)
                .HasForeignKey(v => v.HozzaszolasID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
