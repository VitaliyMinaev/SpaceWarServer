using Hashing;
using SpaceWar.Api.Domain.Abstract;

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
    public AccountDomain(string username, string email, string password)
    {
        Username = username;
        Email = email;
        PasswordHash = Sha256Alghorithm.GenerateHash(password);
        Roles = new string[] { "user" };
    }
    public AccountDomain(string username, string email, string password, string[] roles)
    {
        Username = username;
        Email = email;
        PasswordHash = Sha256Alghorithm.GenerateHash(password);
        Roles = roles;
    }
    public void ChangePassword(string newPassword)
    {
        PasswordHash = Sha256Alghorithm.GenerateHash(newPassword);
    }
}