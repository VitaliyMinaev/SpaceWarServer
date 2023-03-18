using SpaceWar.Api.Installers.Extensions;

namespace SpaceWar.Api;

public class Startup
{
    private readonly ILogger<Startup> _logger;
    public IConfiguration Configuration
    {
        get;
    }
    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
        this.Configuration = configuration;
        _logger = logger;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServiceInAssembly(Configuration, _logger);
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors();

        app.MapControllers();

        if(Environment.GetEnvironmentVariable("ConnectionString") != null)
            DatabasePreparations.Migrate(app, app.Logger);

        app.Run();
    }
}
