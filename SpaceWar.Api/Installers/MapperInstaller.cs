using SpaceWar.Api.Installers.Abstract;

namespace SpaceWar.Api.Installers;

public class MapperInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration,
        ILogger<Startup> logger)
    {
        services.AddAutoMapper(typeof(Startup));
    }
}
