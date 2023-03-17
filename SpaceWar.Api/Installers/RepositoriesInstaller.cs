using SpaceWar.Api.Installers.Abstract;
using SpaceWar.Api.Repositories;
using SpaceWar.Api.Repositories.Abstract;

namespace SpaceWar.Api.Installers;

public class RepositoriesInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration,
        ILogger<Startup> logger)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}
