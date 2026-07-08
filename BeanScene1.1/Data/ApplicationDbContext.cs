using BeanScene1._1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeanScene1._1.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<BeanScene1._1.Models.Menu> Menu { get; set; } = default!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(l => new { l.RoleId, l.UserId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(l => new { l.LoginProvider, l.UserId });
            modelBuilder.Entity<Area>()
                .HasMany(t => t.Tables)
                .WithOne(a => a.Areas)
                .HasForeignKey(a => a.AreaId);
           
            modelBuilder.Entity<ReservationTable>()
            .HasKey(rt => new { rt.TableId, rt.ReservationId });
            modelBuilder.Entity<ReservationTable>()
            .HasOne(rt => rt.Reservations)
            .WithMany(r => r.ReservationTables)
            .HasForeignKey(rt => rt.ReservationId);
            modelBuilder.Entity<ReservationTable>()
            .HasOne(rt => rt.Table)
            .WithMany(r => r.ReservationTables)
            .HasForeignKey(rt => rt.TableId);


            modelBuilder.Entity<Reservations>()
                .HasOne(u => u.AppUser)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Table> Tables { get; set; }

        public DbSet<Reservations> Reservations { get; set; } = default!;
        public DbSet<Menu> Menus { get; set; } = default!;

        public DbSet<ReservationTable> ReservationTables { get; set; } = default!;
        public DbSet<BeanScene1._1.Models.Report> Report { get; set; } = default!;
    }
}
