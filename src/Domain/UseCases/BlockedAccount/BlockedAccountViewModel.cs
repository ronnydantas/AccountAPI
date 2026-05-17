using Domain.Entities;

namespace Domain.UseCases.BlockedAccount;

public record class BlockedAccountViewModel(Guid Userid, AccountStatus Status);