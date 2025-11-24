// Ubicación: Alexis.App.Domain/Entities/Response.cs
using System.ComponentModel.DataAnnotations;

namespace AlexisApp.Domain.Entities
{
    public class Response
    {
        [Key]
        public Guid ResponseId { get; set; } = Guid.NewGuid();
        public Guid TicketId { get; set; } // Foreign Key
        public Guid ResponderId { get; set; } // Foreign Key
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Propiedades de navegación
        public Ticket Ticket { get; set; }
        public User Responder { get; set; }
    }
}