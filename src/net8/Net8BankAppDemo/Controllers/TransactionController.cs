using Net8BankAppDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Net8BankAppDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly BankContext _context;

    public TransactionController(BankContext context)
    {
        _context = context;
    }

    [HttpGet("{accountId}")]
    public async Task<ActionResult<string>> Get(int accountId)
    {
        // LINQ with EF Core - secure parameterized queries vs raw SQL concatenation
        var transactions = await _context.Transactions
            .Where(t => t.FromAccount == accountId.ToString() || t.ToAccount == accountId.ToString())
            .ToListAsync();

        var result = "Transaction History:\n";
        foreach (var transaction in transactions)
        {
            result += $"From: {transaction.FromAccount}, To: {transaction.ToAccount}, Amount: {transaction.Amount}\n";
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] TransferRequest request)
    {
        // Strongly-typed request model vs multiple [FromBody] parameters
        var transaction = new Transaction
        {
            FromAccount = request.From,
            ToAccount = request.To,
            Amount = request.Amount
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(); // Safe parameterized insert

        return Ok("Transfer completed.");
    }
}

public class TransferRequest
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}