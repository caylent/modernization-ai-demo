using Microsoft.EntityFrameworkCore;

namespace Net8BankAppDemo.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}

public class Account
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }
    public string Email { get; set; } = string.Empty;
}

public class Transaction
{
    public int Id { get; set; }
    public string FromAccount { get; set; } = string.Empty;
    public string ToAccount { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}