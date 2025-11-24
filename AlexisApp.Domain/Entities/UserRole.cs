// Ubicación: Alexis.App.Domain/Entities/UserRole.cs
namespace AlexisApp.Domain.Entities
{
    // Esta clase representa la tabla intermedia (muchos a muchos)
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        
        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}