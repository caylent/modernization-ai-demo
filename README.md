# modernization-ai-demo

This project is a demo to showcase how Gen AI can speed up legacy code migrations with an example of .NET Framework application migrated to .NET 8.

## Project Structure

- `src/net48/LegacyBankAppDemo/` - Original .NET Framework 4.8 Web API
- `src/net8/Net8BankAppDemo/` - Modernized .NET 8 Web API

## How to Run

### .NET Framework (Legacy)
```bash
cd src/net48/LegacyBankAppDemo
# Open in Visual Studio and run with IIS Express
```

### .NET 8 (Modernized)
```bash
cd src/net8/Net8BankAppDemo
dotnet restore
dotnet run --environment Development
```

Access the .NET 8 API at `https://localhost:5001` with Swagger UI at `/swagger`.

## License

MIT License