// Ubicación: AlexisApp/Controllers/AuthController.cs

using AlexisApp.Application.DTOs;
using AlexisApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlexisApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La ruta será /api/Auth
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                return Ok(new { Token = token }); // Devuelve el token
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);
                return Ok(new { Token = token }); // Devuelve el token
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}