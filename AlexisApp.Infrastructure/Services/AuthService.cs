// Ubicación: AlexisApp.Infrastructure/Services/AuthService.cs

using AlexisApp.Application.DTOs;
using AlexisApp.Application.Interfaces;
using AlexisApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


// Asegúrate de tener los 'usings' de tus namespaces correctos
// ej: using AlexisApp.Domain.Entities;

namespace AlexisApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        // Inyectamos IUnitOfWork para acceder a la BD
        // e IConfiguration para leer el appsettings.json
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            // 1. Verificamos si el usuario ya existe
            var existingUser = (await _unitOfWork.Users.FindAsync(u => u.Username == registerDto.Username)).FirstOrDefault();
            if (existingUser != null)
            {
                throw new ApplicationException("El nombre de usuario ya existe.");
            }

            // 2. Encriptamos la contraseña (¡Punto opcional del lab!)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };

            // 3. Guardamos el usuario en la BD
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            // 4. Devolvemos un token para el nuevo usuario
            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // 1. Buscamos al usuario
            var user = (await _unitOfWork.Users.FindAsync(u => u.Username == loginDto.Username)).FirstOrDefault();
            if (user == null)
            {
                throw new ApplicationException("Usuario o contraseña incorrectos.");
            }

            // 2. Verificamos la contraseña encriptada
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new ApplicationException("Usuario o contraseña incorrectos.");
            }

            // 3. Si todo es correcto, generamos el token
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Aquí podrías añadir claims de Roles si los cargas desde la BD
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // El token expira en 1 hora
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}