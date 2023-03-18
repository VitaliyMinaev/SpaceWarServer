using SpaceWar.Api.Installers.Abstract;
using SpaceWar.Api.Services;
using SpaceWar.Api.Services.Abstract;
using SpaceWar.Api.Services.Strategy;
using SpaceWar.Api.Services.Strategy.Abstract;

namespace SpaceWar.Api.Installers;

public class ServicesInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration,
        ILogger<Startup> logger)
    {
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IGeneratorGwtStrategy, DefaultGeneratorGwtStrategy>();
        services.AddScoped<IHashService, Sha256HashService>();
        services.AddSingleton<IClaimParser, ClaimParser>();
    }
}
