using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Domain.UseCases.AddAccount;

public class AddAccountUseCase : IRequestHandler<AddAccountCommand, AddAccountViewModel>
{
    private readonly IAccountRepository _accountRepository;

    public AddAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AddAccountViewModel> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),

            UserId = request.UserId,

            Agency = "0001",

            AccountNumber = GenerateAccountNumber(),

            Balance = 0,

            Status = AccountStatus.Active,

            Type = (AccountType)request.Type,

            CreatedAt = DateTime.UtcNow
        };

        await _accountRepository.AddAsync(account);

        var viewModel = new AddAccountViewModel(
            account.Id,
            account.UserId,
            account.Agency,
            account.AccountNumber,
            account.Balance,
            account.CreatedAt
        );

        return viewModel;
    }

    private static string GenerateAccountNumber()
    {
        var random = new Random();

        return random.Next(100000, 999999).ToString();
    }
}