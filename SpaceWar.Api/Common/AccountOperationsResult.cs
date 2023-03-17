using SpaceWar.Api.Entities;

namespace SpaceWar.Api.Common;

public class AccountOperationsResult
{
    public bool Success { get; set; }
    public string[]? Errors { get; set; }
    public AccountEntity? Account { get; set; }
}
