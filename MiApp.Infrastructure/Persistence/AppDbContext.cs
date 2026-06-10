using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<TicketZone> TicketZones { get; set; }
        public DbSet<TicketPurchase> TicketPurchases { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Evento
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                // Configurar la navegación hacia Zones
                entity.Metadata.FindNavigation(nameof(Event.Zones))
                    ?.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            // Configuración de TicketZone
            modelBuilder.Entity<TicketZone>(entity =>
            {
                entity.HasKey(tz => tz.Id);
                entity.HasOne(tz => tz.Event)
                      .WithMany(e => e.Zones)
                      .HasForeignKey(tz => tz.EventId);
            });

            // Configuración de TicketPurchase
            modelBuilder.Entity<TicketPurchase>(entity =>
            {
                entity.HasKey(tp => tp.Id);
                entity.HasOne(tp => tp.Event).WithMany().HasForeignKey(tp => tp.EventId);
                entity.HasOne(tp => tp.Zone).WithMany(tz => tz.Purchases).HasForeignKey(tp => tp.ZoneId);
            });

            // Configuración de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
            });
        }
    }
}
