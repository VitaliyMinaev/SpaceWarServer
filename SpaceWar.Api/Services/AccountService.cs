using AutoMapper;
using SpaceWar.Api.Common;
using SpaceWar.Api.Domain;
using SpaceWar.Api.Entities;
using SpaceWar.Api.Repositories.Abstract;
using SpaceWar.Api.Services.Abstract;
using SpaceWar.Api.Services.Strategy.Abstract;

namespace SpaceWar.Api.Services;

public class AccountService : IAccountService
{
    private readonly IGeneratorGwtStrategy _jwtGenerator;
    private readonly IAccountRepository _accountRepository;
    private readonly IHashService _hashGenerator;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IMapper mapper, IGeneratorGwtStrategy jwtGenerator, IHashService hashGenerator)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
        _hashGenerator = hashGenerator;
    }

    public async Task<AuthenticationResult> RegisterAsync(AccountDomain account)
    {
        AccountOperationsResult result = await _accountRepository
            .CreateAccountAsync(_mapper.Map<AccountEntity>(account));

        if (result.Success == false)
        {
            return new AuthenticationResult
            {
                AccessToken = null,
                Success = false,
                Errors = result.Errors
            };
        }

        return new AuthenticationResult
        {
            Success = true,
            AccessToken = _jwtGenerator.GenerateGwt(account),
            Errors = null
        };
    }
    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        string passwordHash = _hashGenerator.GenerateHash(password);
        var exists = await _accountRepository.GetAccountByEmailAndPasswordAsync(email, passwordHash);

        if (exists == null)
        {
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] { "Invalid email or password" },
                AccessToken = null
            };
        }

        return new AuthenticationResult
        {
            Success = true,
            AccessToken = _jwtGenerator.GenerateGwt(_mapper.Map<AccountDomain>(exists)),
            Errors = null
        };
    }
    public async Task<ChangeAccountDataResult> ChangeUsernameAsync(string email, string password, string newUsername)
    {
        string passwordHash = _hashGenerator.GenerateHash(password);
        var result = await _accountRepository.ChangeUsernameAsync(email, passwordHash, newUsername);

        if(result.Success == false)
        {
            return new ChangeAccountDataResult { Success = false, Errors = result.Errors, AccessToken = null };
        }

        return new ChangeAccountDataResult 
        { 
            Success = true, 
            Errors = null, 
            AccessToken = _jwtGenerator.GenerateGwt(_mapper.Map<AccountDomain>(result.Account)) 
        };
    }
    public async Task<ChangeAccountDataResult> ChangePasswordAsync(Guid accountId, string oldPassword, string newPassword)
    {
        string oldPasswordHash = _hashGenerator.GenerateHash(oldPassword);
        string newPasswordHash = _hashGenerator.GenerateHash(newPassword);
        var result = await _accountRepository.ChangePasswordAsync(accountId, oldPasswordHash, newPasswordHash);

        if(result.Success == false)
        {
            return new ChangeAccountDataResult
            {
                Success = false,
                Errors = result.Errors,
                AccessToken = null
            };
        }

        return new ChangeAccountDataResult
        {
            Success = true,
            Errors = null,
            AccessToken = _jwtGenerator.GenerateGwt(_mapper.Map<AccountDomain>(result.Account))
        };
    }
}