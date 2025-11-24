// Ubicación: Alexis.App.Domain/Entities/Role.cs
using System.ComponentModel.DataAnnotations;

namespace AlexisApp.Domain.Entities
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; } = Guid.NewGuid();
        public string RoleName { get; set; }

        // Propiedad de navegación
        public ICollection<UserRole> UserRoles { get; set; }
    }
}