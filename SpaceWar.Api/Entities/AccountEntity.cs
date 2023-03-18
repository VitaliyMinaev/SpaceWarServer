using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpaceWar.Api.Entities.Abstract;

namespace SpaceWar.Api.Entities;

[Table("Account")]
public class AccountEntity : BaseEntity
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? PasswordHash { get; set; }

    public ICollection<RoleEntity>? Roles { get; set; }
}