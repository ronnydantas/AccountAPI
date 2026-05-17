using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account?> ConsultarPorId(Guid id);
    Task<Account?> ConsultarPorUserId(Guid userId);
    Task<IEnumerable<Account>> ConsultarTodos();
}