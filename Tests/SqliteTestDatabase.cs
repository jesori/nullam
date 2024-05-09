using System.Data;
using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class SqliteTestDatabase : ITestDatabase
{
    private readonly string _connectionString;
    private readonly SqliteConnection _connection;

    public SqliteTestDatabase()
    {
        _connectionString = "DataSource=:memory:";
        _connection = new SqliteConnection(_connectionString);
    }

    public async Task InitialiseAsync()
    {
        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        var context = new AppDbContext(options);

        context.Database.Migrate();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await InitialiseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
