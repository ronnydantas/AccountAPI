using Domain.Entities;

namespace Domain.UseCases.GetAccountByUserId;

public class GetAccountByUserIdViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Agency { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;
    public AccountStatus Status { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }


    public GetAccountByUserIdViewModel(Guid id, Guid userId, string agency, string accountNumber, AccountStatus status, decimal balance, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Agency = agency;
        AccountNumber = accountNumber;
        Status = status;
        Balance = balance;
        CreatedAt = createdAt;
    }
}
