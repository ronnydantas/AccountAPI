using Domain.Entities;
using Domain.UseCases.AddAccount;
using MediatR;
using System.Text.Json.Serialization;

namespace Domain.UseCases.BlockedAccount;

public class BlockedAccountCommand : IRequest<BlockedAccountViewModel>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public AccountStatus Status { get; set; }
}
