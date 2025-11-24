// Ubicación: Alexis.App.Domain/Entities/User.cs
using System.ComponentModel.DataAnnotations;

namespace AlexisApp.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Propiedades de navegación para las relaciones
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}