using MediatR;

namespace Domain.UseCases.GetAccountByUserId;

public record GetAccountByUserIdQuery(Guid UserId) : IRequest<GetAccountByUserIdViewModel>;
