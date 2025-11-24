// Ubicación: AlexisApp.Infrastructure/Persistence/ApplicationDbContextFactory.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

// ¡ASEGÚRATE DE QUE EL NAMESPACE SEA EL CORRECTO!
namespace AlexisApp.Infrastructure.Persistence
{
    // Esta clase le dice a las herramientas 'dotnet ef' CÓMO crear tu DbContext
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 1. Buscamos el appsettings.json
            // Obtenemos la ruta actual (que será .../AlexisApp.Infrastructure)
            string basePath = Directory.GetCurrentDirectory();
            
            // Salimos de /Infrastructure y entramos a /AlexisApp para encontrar el appsettings.json
            // C:\...\AlexisApp.Infrastructure -> C:\...\AlexisApp\appsettings.json
            string path = Path.Combine(basePath, "..", "AlexisApp", "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path) // Cargamos el appsettings.json
                .Build();

            // 2. Leemos la cadena de conexión
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 3. Creamos las opciones para el DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // 4. Le decimos que use SQL Server con esa cadena de conexión
            optionsBuilder.UseSqlServer(connectionString);

            // 5. Devolvemos la nueva instancia del DbContext
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}