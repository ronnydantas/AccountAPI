using MediatR;

namespace Domain.UseCases.AddAccount;

public class AddAccountCommand : IRequest<AddAccountViewModel>
{
    public Guid UserId { get; set; }

    public int Type { get; set; }
}
