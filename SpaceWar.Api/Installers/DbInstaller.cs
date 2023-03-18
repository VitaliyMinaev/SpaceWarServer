using Microsoft.EntityFrameworkCore;
using SpaceWar.Api.Installers.Abstract;
using SpaceWar.Api.Persistence;

namespace SpaceWar.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
    {
        services.AddDbContext<ApplicationDataContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
        });
        logger.LogInformation("Application is using sqlite database");
    }
}
