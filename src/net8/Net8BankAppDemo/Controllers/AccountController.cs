using Net8BankAppDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace Net8BankAppDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly BankContext _context;

    // Dependency injection replaces manual connection string management
    public AccountController(BankContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get(int id)
    {
        // EF Core eliminates SQL injection vulnerabilities from legacy code
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound(); // Proper HTTP status codes

        return Ok($"Account: {account.AccountId}, Balance: {account.Balance}");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> Put(int id, [FromBody] string email)
    {
        // Async/await for better scalability vs synchronous legacy code
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound();

        account.Email = email;
        await _context.SaveChangesAsync(); // Parameterized queries prevent SQL injection
        
        return Ok("Account updated.");
    }
}