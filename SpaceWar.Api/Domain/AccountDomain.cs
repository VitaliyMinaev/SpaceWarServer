using SpaceWar.Api.Domain.Abstract;
using SpaceWar.Api.Services.Abstract;

namespace SpaceWar.Api.Domain;

public class AccountDomain : BaseDomain
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string[] Roles { get; set; }

    public AccountDomain()
    {
        Username = String.Empty;
        Email = String.Empty;
        PasswordHash = String.Empty;
        Roles = new string[0];
    }
    public AccountDomain(string username, string email, string password, IHashService hashService)
    {
        Username = username;
        Email = email;
        PasswordHash = hashService.GenerateHash(password);
        Roles = new string[] { "user" };
    }
    public AccountDomain(string username, string email, string password, string[] roles, IHashService hashService)
    {
        Username = username;
        Email = email;
        PasswordHash = hashService.GenerateHash(password);
        Roles = roles;
    }
}