// Ubicación: AlexisApp.Application/Interfaces/IAuthService.cs
using AlexisApp.Application.DTOs; // <-- Añade este using

namespace AlexisApp.Application.Interfaces
{
    public interface IAuthService
    {
        // Añadimos métodos para registrar y loguear
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}