using Microsoft.EntityFrameworkCore;
using SpaceWar.Api.Entities;

namespace SpaceWar.Api.Persistence;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
        : base(options)
    { }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
}
