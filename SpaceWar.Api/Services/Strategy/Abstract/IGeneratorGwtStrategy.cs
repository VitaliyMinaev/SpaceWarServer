using SpaceWar.Api.Domain;

namespace SpaceWar.Api.Services.Strategy.Abstract;

public interface IGeneratorGwtStrategy
{
    string GenerateGwt(AccountDomain account);
}
