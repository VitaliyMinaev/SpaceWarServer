using Microsoft.EntityFrameworkCore;
using SpaceWar.Api.Installers.Abstract;
using SpaceWar.Api.Persistence;

namespace SpaceWar.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
    {
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        if(connectionString == null)
            logger.LogInformation("There is no connection string as environment variable. " +
                    "App will use inmemory database");
        else 
            logger.LogInformation("Connection string has been found as environment variable. " +
                    "App will use sql server");

        services.AddDbContext<ApplicationDataContext>(options =>
        {
            if(connectionString == null)
            {
                options.UseInMemoryDatabase("InMemoryDb");
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });
    }
}
