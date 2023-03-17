using System.ComponentModel.DataAnnotations.Schema;
using SpaceWar.Api.Entities.Abstract;

namespace SpaceWar.Api.Entities;

[Table("Role")]
public class RoleEntity : BaseEntity
{
    public string? Role { get; set; }
    public ICollection<AccountEntity>? Accounts { get; set; }
}