using Domain.Interfaces;
using MediatR;

namespace Domain.UseCases.GetAccountByUserId;

public class GetAccountByUserIdUseCase : IRequestHandler<GetAccountByUserIdQuery, GetAccountByUserIdViewModel>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByUserIdUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<GetAccountByUserIdViewModel> Handle(GetAccountByUserIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.ConsultarPorUserId(request.UserId);

        if (account == null)
        {
            return null;
        }
        var accountViewModel = new GetAccountByUserIdViewModel(account.Id, account.UserId, account.Agency, account.AccountNumber, account.Status, account.Balance, account.CreatedAt);

        return accountViewModel;

    }
}
