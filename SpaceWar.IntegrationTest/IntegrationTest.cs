using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using SpaceWar.Api;
using SpaceWar.Api.Contracts.v1;
using SpaceWar.Api.Contracts.v1.Requests;
using SpaceWar.Api.Contracts.v1.Responses;
using SpaceWar.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using SpaceWar.IntegrationTests.Environment;

namespace SpaceWar.IntegrationTests;

public class IntegrationTests
{
    protected readonly HttpClient httpClient;
    private static string _accessToken = null;
    public IntegrationTests()
    {
        var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDataContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<ApplicationDataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });
        httpClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        if(_accessToken == null)
        {
            _accessToken = await GetJwtAsync();
        }

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", 
            _accessToken);
    }
    private async Task<string> GetJwtAsync()
    {
        var response = await httpClient.PostAsJsonAsync(ApiRoutes.Account.Register, new AccountRegistrationRequest
        {
            Username = AccountData.Username,
            Email = AccountData.Email,
            Password = AccountData.Password
        });

        var json = await response.Content.ReadAsStringAsync();
        var responseToken = JsonSerializer.Deserialize<AuthorizationSuccessResponse>(json);
        return responseToken.access_token;
    }
}
