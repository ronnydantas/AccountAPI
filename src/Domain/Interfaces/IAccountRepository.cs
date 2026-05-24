using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account?> ConsultarPorId(Guid id);
    Task<Account?> CriarContaComPreDados(ClienteConsumer cliente);
    Task<Account?> AtualizarStatus(Guid userId, AccountStatus status);
    Task<Account?> ConsultarPorUserId(Guid userId);
    Task<IEnumerable<Account>> ConsultarTodos();
}