using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        AppDbContextInitializer initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

        await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly ILogger<AppDbContextInitializer> _logger;

    public AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Todos.Any())
        {
            await _context.Todos.AddRangeAsync(TodoGenerator.GenerateTodos());

            await _context.SaveChangesAsync();
        }
    }
}

internal static class TodoGenerator
{
    private static readonly (string[] Prefixes, string[] Suffixes)[] Parts =
    {
        (new[] { "Walk the", "Feed the" }, new[] { "dog", "cat", "goat" }),
        (new[] { "Do the", "Put away the" }, new[] { "groceries", "dishes", "laundry" }),
        (new[] { "Clean the" }, new[] { "bathroom", "pool", "blinds", "car" })
    };

    internal static IEnumerable<Todo> GenerateTodos(int count = 10)
    {
        int titleCount = Parts.Sum(row => row.Prefixes.Length * row.Suffixes.Length);
        (int Row, int Prefix, int Suffix)[] titleMap = new (int Row, int Prefix, int Suffix)[titleCount];
        int mapCount = 0;
        for (int i = 0; i < Parts.Length; i++)
        {
            string[] prefixes = Parts[i].Prefixes;
            string[] suffixes = Parts[i].Suffixes;
            for (int j = 0; j < prefixes.Length; j++)
            {
                for (int k = 0; k < suffixes.Length; k++)
                {
                    titleMap[mapCount++] = (i, j, k);
                }
            }
        }

        Random.Shared.Shuffle(titleMap);

        for (int id = 1; id <= count; id++)
        {
            (int rowIndex, int prefixIndex, int suffixIndex) = titleMap[id];
            (string[] prefixes, string[] suffixes) = Parts[rowIndex];
            yield return new Todo
            {
                Id = id,
                Title = string.Join(' ', prefixes[prefixIndex], suffixes[suffixIndex]),
                DueBy = Random.Shared.Next(-200, 365) switch
                {
                    < 0 => null,
                    var days => DateTime.Now.AddDays(days)
                }
            };
        }
    }


    private static void Shuffle<T>(this Random random, IList<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        int n = values.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int j = random.Next(i, n);

            if (j != i)
            {
                (values[i], values[j]) = (values[j], values[i]);
            }
        }
    }
}
