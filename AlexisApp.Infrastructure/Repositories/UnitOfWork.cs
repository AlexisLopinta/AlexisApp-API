// Ubicación: AlexisApp.Infrastructure/Repositories/UnitOfWork.cs
using AlexisApp.Application.Interfaces;
using AlexisApp.Domain.Entities;
using AlexisApp.Infrastructure.Persistence;

namespace AlexisApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        // Propiedades 'public' con 'private set'
        public IRepository<User> Users { get; private set; }
        public IRepository<Ticket> Tickets { get; private set; }
        public IRepository<Role> Roles { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            
            // Inicializamos los repositorios, pasándoles el DbContext
            Users = new Repository<User>(_context);
            Tickets = new Repository<Ticket>(_context);
            Roles = new Repository<Role>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            // Guarda todos los cambios hechos en este 'UnitOfWork'
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // Libera la conexión a la BD
            _context.Dispose();
        }
    }
}