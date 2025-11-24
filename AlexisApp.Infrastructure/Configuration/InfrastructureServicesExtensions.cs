using Alexis.App.Application.Interfaces;
using AlexisApp.Application.Interfaces;

using AlexisApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using AlexisApp.Infrastructure.Persistence;
using AlexisApp.Infrastructure.Repositories;




namespace AlexisApp.Infrastructure.Configuration
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuration de DbContext para SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString); // < SQL Server
            });

            // Registrar servicios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
            services.AddScoped<IActivityService, ActivityService>();

            return services; // ← Faltaba este return
        }
    }
}