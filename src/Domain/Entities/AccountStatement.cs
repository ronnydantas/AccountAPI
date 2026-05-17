namespace Domain.Entities;

public class AccountStatement
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public decimal CurrentBalance { get; set; }

    public DateTime GeneratedAt { get; set; }
}