// Ubicación: AlexisApp.Infrastructure/Repositories/Repository.cs

using AlexisApp.Application.Interfaces; // <-- La interfaz que acabamos de crear
using AlexisApp.Infrastructure.Persistence; // <-- Tu DbContext
using Microsoft.EntityFrameworkCore; // <-- Para usar .FindAsync, .ToListAsync, etc.
using System.Linq.Expressions;

namespace AlexisApp.Infrastructure.Repositories
{
    // Esta clase implementa la interfaz genérica IRepository<T>
    public class Repository<T> : IRepository<T> where T : class
    {
        // Necesitamos el DbContext para hablar con la BD
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 'Set<T>()' le dice a EF en qué tabla (DbSet) debe trabajar
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Es el equivalente a un 'WHERE' en SQL
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            // Update y Remove no son 'async' porque solo marcan la entidad
            // como 'Modificada' o 'Eliminada'. El guardado (SaveChangesAsync)
            // es el que hace el trabajo asíncrono.
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}