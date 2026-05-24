using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(AccountContext context) : base(context)
    {
    }

    public async Task<Account?> AtualizarStatus(Guid UserId, AccountStatus status)
    {
        var account = await _context.Contas.FirstOrDefaultAsync(c => c.UserId == UserId);

        if (account == null)
            return null;

        account.Status = status;

        _context.Contas.Update(account);

        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Account?> ConsultarPorId(Guid id)
    {
        return await _context.Contas.FindAsync(id);
    }

    public async Task<Account?> ConsultarPorUserId(Guid userId)
    {
        return await _context.Contas.FirstOrDefaultAsync(c => c.UserId == userId);
    }
    public async Task<IEnumerable<Account>> ConsultarTodos()
    {
        return await _context.Contas.ToListAsync();
    }

    public async Task<Account?> CriarContaComPreDados(ClienteConsumer cliente)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),

            UserId = Guid.Parse(cliente.Id),

            Agency = "0001",

            AccountNumber = GenerateAccountNumber(),

            Balance = 0,

            Status = AccountStatus.Active,

            Type = AccountType.Checking,

            CreatedAt = DateTime.UtcNow
        };

        await _context.Contas.AddAsync(account);

        await _context.SaveChangesAsync();

        return account;
    }

    private static string GenerateAccountNumber()
    {
        var random = new Random();

        return random.Next(100000, 999999).ToString();
    }
}
