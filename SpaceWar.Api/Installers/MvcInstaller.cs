using SpaceWar.Api.Installers.Abstract;

namespace SpaceWar.Api.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration,
        ILogger<Startup> logger)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}
