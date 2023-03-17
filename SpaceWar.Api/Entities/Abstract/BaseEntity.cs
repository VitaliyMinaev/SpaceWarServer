using System.ComponentModel.DataAnnotations;

namespace SpaceWar.Api.Entities.Abstract;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreationTime { get; set; }
}