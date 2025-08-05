using Net8BankAppDemo.Data;
using Microsoft.EntityFrameworkCore;

// Modern .NET 8 minimal hosting model - replaces Global.asax.cs
var builder = WebApplication.CreateBuilder(args);

// Built-in dependency injection - no need for external containers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Built-in OpenAPI documentation

// Entity Framework Core with dependency injection
builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Automatic Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();