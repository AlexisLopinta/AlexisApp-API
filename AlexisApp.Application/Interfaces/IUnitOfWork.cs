// Ubicación: AlexisApp.Application/Interfaces/IUnitOfWork.cs

using AlexisApp.Domain.Entities; 

namespace AlexisApp.Application.Interfaces
{
    // La interfaz debe estar DENTRO del namespace
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<Role> Roles { get; }

        Task<int> CompleteAsync();
    }
    
} // <-- La llave de cierre del namespace va AQUÍ, al final del archivo.