using Hashing;
using SpaceWar.Api.Services.Abstract;

namespace SpaceWar.Api.Services;

public class Sha256HashService : IHashService
{
    public string GenerateHash(string input)
    {
        return Sha256Alghorithm.GenerateHash(input);
    }
}