// Ubicación: Alexis.App.Domain/Entities/Ticket.cs
using System.ComponentModel.DataAnnotations;

namespace AlexisApp.Domain.Entities
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } // Foreign Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // "abierto", "en_proceso", "cerrado"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ClosedAt { get; set; }

        // Propiedades de navegación
        public User User { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}