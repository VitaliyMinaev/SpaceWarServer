using System.ComponentModel.DataAnnotations;

namespace SpaceWar.Api.Contracts.v1.Requests;

public class AccountLoginRequest
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}