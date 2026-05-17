namespace Domain.Entities;

public class Account
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Agency { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public AccountType Type { get; set; }

    public AccountStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}