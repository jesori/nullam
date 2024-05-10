using System.Security.Cryptography;
using Domain.Entities;
using Domain.Enums;
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
        var event1 = new Event()
        {
            Id = Guid.NewGuid(),
            Name = "Galactic Gala",
            Date = DateTime.Now + TimeSpan.FromDays(90),
            Location = "Earth Orbit",
            Info =
                "Embark on a journey beyond the stars at the Galactic Gala hosted aboard Spaceport Alpha, orbiting Earth. This exclusive event invites space enthusiasts, scientists, and adventurers alike to celebrate humanity's exploration of the cosmos.",
        };

        var event2 = new Event()
        {
            Id = Guid.NewGuid(),
            Name = "Galactic Gala",
            Date = DateTime.Now - TimeSpan.FromDays(90),
            Location = "Woods",
            Info =
                "Step into a world of magic and wonder at the \"Enchanted Forest Festival\" nestled within the mystical Whispering Woods of Meadowvale. This immersive outdoor event invites attendees to escape the mundane and embrace the enchantment of nature"
        };
        var p1 = new PrivateParticipant()
        {
            Id = Guid.NewGuid(),
            FirstName = "Participant1",
            LastName = "Lastname1",
            IdNumber = "37826937641",
            PaymentMethod = PaymentMethod.ByCard
        };

        var p2 = new PrivateParticipant()
        {
            Id = Guid.NewGuid(),
            FirstName = "Participant2",
            LastName = "Lastname2",
            IdNumber = "49857367852",
            PaymentMethod = PaymentMethod.InCache
        };
        var p3 = new BusinessParticipant()
        {
            Id = Guid.NewGuid(),
            Name = "Ettevotte OU",
            IdNumber = "346GD7849",
            ParticipantsNumber = 12,
            PaymentMethod = PaymentMethod.ByCard
        };

        var ep1 = new EventParticipant()
        {
            EventId = event1.Id,
            PrivateParticipantId = p1.Id
        };
        var ep2 = new EventParticipant()
        {
            EventId = event1.Id,
            BusinessParticipantId = p3.Id
        };
        var ep3 = new EventParticipant()
        {
            EventId = event2.Id,
            PrivateParticipantId = p2.Id
        };

        if (!_context.Events.Any())
        {
            await _context.Events.AddAsync(event1);
            await _context.Events.AddAsync(event2);
        }

        if (!_context.PrivateParticipants.Any())
        {
            await _context.PrivateParticipants.AddAsync(p1);
            await _context.PrivateParticipants.AddAsync(p2);
        }

        if (!_context.BusinessParticipants.Any())
        {
            await _context.BusinessParticipants.AddAsync(p3);
        }

        if (!_context.EventParticipants.Any())
        {
            await _context.EventParticipants.AddAsync(ep1);
            await _context.EventParticipants.AddAsync(ep2);
            await _context.EventParticipants.AddAsync(ep3);
        }
        await _context.SaveChangesAsync();

    }
}
