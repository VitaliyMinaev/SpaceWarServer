using Microsoft.EntityFrameworkCore;
using SpaceWar.Api.Common;
using SpaceWar.Api.Entities;
using SpaceWar.Api.Persistence;
using SpaceWar.Api.Repositories.Abstract;

namespace SpaceWar.Api.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDataContext _context;

    public AccountRepository(ApplicationDataContext context)
    {
        _context = context;
    }

    public async Task<AccountOperationsResult> CreateAccountAsync(AccountEntity account)
    {
        var existingUser = await FindByEmailAsync(account.Email);

        if(existingUser != null)
        {
            return new AccountOperationsResult
            {
                Success = false,
                Errors = new string[]
                {
                    "User with this email address already exist"
                }
            };
        }

        await _context.Accounts.AddAsync(account);
        int updated = await _context.SaveChangesAsync();

        return updated > 0 ? new AccountOperationsResult { Success = true, Account = account } : 
            new AccountOperationsResult { Success = false, 
                Errors = new string[] { "Initial server error", "Can not create an account" } };
    }
    
    public async Task<AccountEntity?> GetAccountByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);
    }
    
    public async Task<AccountOperationsResult> ChangeUsernameAsync(string email, string passwordHash, string newUsername)
    {
        var account = await GetAccountByEmailAndPasswordAsync(email, passwordHash);

        if (account == null)
        {
            return new AccountOperationsResult 
            { 
                Success = false, 
                Errors = new string[] { "User with given email address and password does not exist" }
            };
        }

        account.Username = newUsername;
        int updated = await _context.SaveChangesAsync();

        return updated > 0 ? new AccountOperationsResult { Success = true, Errors = null, Account = account }
           : new AccountOperationsResult { Success = false, Errors = new[] { "Initial server error", "Can not update account data" } };
    }
    public async Task<AccountOperationsResult> ChangePasswordAsync(Guid accountId, string oldPasswordHash, string newPasswordHash)
    {
        var account = await FindByIdAsync(accountId);

        if (account == null)
        {
            return new AccountOperationsResult
            {
                Success = false,
                Errors = new string[]
                {
                    "User with given id does not exist"
                }
            };
        }

        if(account.PasswordHash != oldPasswordHash)
        {
            return new AccountOperationsResult
            {
                Success = false,
                Errors = new string[]
                {
                    "Wrong password"
                }
            };
        }

        account.PasswordHash = newPasswordHash;
        int updated = await _context.SaveChangesAsync();

        return updated > 0 ? new AccountOperationsResult { Success = true, Errors = null, Account = account } 
         : new AccountOperationsResult { Success = false, Errors = new[] { "Initial server error", "Can not update account data" } };
    }

    public async Task<AccountEntity?> FindByEmailAsync(string email)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<AccountEntity?> FindByIdAsync(Guid accountId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
    }
}