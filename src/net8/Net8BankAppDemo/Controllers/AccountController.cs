using Net8BankAppDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace Net8BankAppDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly BankContext _context;

    public AccountController(BankContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound();

        return Ok($"Account: {account.AccountId}, Balance: {account.Balance}");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> Put(int id, [FromBody] string email)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return NotFound();

        account.Email = email;
        await _context.SaveChangesAsync();
        
        return Ok("Account updated.");
    }
}