// Ubicación: Alexis.App.Infrastructure/Persistence/ApplicationDbContext.cs

using AlexisApp.Domain.Entities; // <-- Importante: El 'using' de tu Dominio
using Microsoft.EntityFrameworkCore;

namespace AlexisApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Añade un DbSet por cada entidad que creaste
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Configuración de Relaciones (basado en tu SQL) ---

            // Configurar la clave primaria compuesta para UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Relación User <-> UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // Relación Role <-> UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Relación Ticket <-> User
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // O .Cascade si prefieres

            // Relación Response <-> User (El "Responder")
            modelBuilder.Entity<Response>()
                .HasOne(r => r.Responder)
                .WithMany(u => u.Responses)
                .HasForeignKey(r => r.ResponderId)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada

            // Relación Response <-> Ticket
            modelBuilder.Entity<Response>()
                .HasOne(r => r.Ticket)
                .WithMany(t => t.Responses)
                .HasForeignKey(r => r.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // Borra respuestas si se borra el ticket
        }
    }
}