namespace Tests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new SqliteTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
