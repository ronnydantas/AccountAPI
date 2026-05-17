using Domain.Interfaces;
using MediatR;

namespace Domain.UseCases.BlockedAccount;

public class BlockedAccountUseCase : IRequestHandler<BlockedAccountCommand, BlockedAccountViewModel>
{
    private readonly IAccountRepository _accountRepository;

    public BlockedAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<BlockedAccountViewModel> Handle(BlockedAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.AtualizarStatus(request.UserId, request.Status);

        if (account == null)
            throw new Exception("Conta não encontrada.");

        return new BlockedAccountViewModel(account.UserId, account.Status);
    }
}