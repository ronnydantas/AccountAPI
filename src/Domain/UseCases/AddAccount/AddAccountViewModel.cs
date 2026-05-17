namespace Domain.UseCases.AddAccount;

public class AddAccountViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Agency { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }


    public AddAccountViewModel(Guid id, Guid userId, string agency, string accountNumber, decimal balance, DateTime createdAt)
    {
        Id = id;
        UserId = userId;    
        Agency = agency;
        AccountNumber = accountNumber;
        Balance = balance;
    }
}