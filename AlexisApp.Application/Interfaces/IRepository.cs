// Ubicación: AlexisApp.Application/Interfaces/IRepository.cs
using System.Linq.Expressions;

namespace AlexisApp.Application.Interfaces
{
    // 'T' será una de tus entidades (User, Ticket, Role, etc.)
    // 'where T : class' significa que T debe ser una clase.
    public interface IRepository<T> where T : class
    {
        // Obtener uno por su ID
        Task<T> GetByIdAsync(Guid id);

        // Obtener todos
        Task<IEnumerable<T>> GetAllAsync();

        // Obtener un listado basado en una condición (ej: buscar por username)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Añadir una nueva entidad
        Task AddAsync(T entity);

        // Actualizar una entidad existente
        void Update(T entity);

        // Eliminar una entidad
        void Remove(T entity);
    }
}