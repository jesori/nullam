using System.ComponentModel.DataAnnotations;
using Application.BusinessParticipants.Queries;
using Application.Events.Commands;
using Application.Events.Queries;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Events.Queries;

public class GetEventTests : BaseTestFixture
{
    [Test]
    public async Task ShouldGetAllEvents()
    {

        await Testing.AddAsync(new Event()
        {
            Name = "Event",
            Date = DateTime.Now,
            Location = "Location"
        });

        var query = new GetAllEventQuery();

        var result = await Testing.SendAsync(query);

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Event");
        result.First().Location.Should().Be("Location");
    }

    [Test]
    public async Task ShouldGetEventById()
    {
        var guid = Guid.NewGuid();
        await Testing.AddAsync(new Event()
        {
            Id = guid,
            Name = "Event",
            Date = DateTime.Now,
            Location = "Location"
        });

        var query = new GetEventByIdQuery()
        {
            Id = guid
        };

        var result = await Testing.SendAsync(query);

        result.Name.Should().Be("Event");
        result.Location.Should().Be("Location");
    }

    [Test]
    public async Task ShouldGetAllBusinessParticipantFromEvent()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();;
        await Testing.AddAsync(new Event()
        {
            Id = guid1,
            Name = "Event",
            Date = DateTime.Now,
            Location = "Location"
        });
        
        await Testing.AddAsync(new BusinessParticipant()
        {
            Id = guid2,
            Name = "BusinessParticipant",
            PaymentMethod = PaymentMethod.ByCard,
            ParticipantsNumber = 2,
            IdNumber = "Number"

        });

        await Testing.AddAsync(new EventParticipant()
        {
            EventId = guid1,
            BusinessParticipantId = guid2,
        
        });

        var query = new GetAllEventBusinessPrticipantsQuery()
        {
            Id = guid1
        };

        var result = await Testing.SendAsync(query);

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("BusinessParticipant");
        result.First().IdNumber.Should().Be("Number");
    }
    
    [Test]
    public async Task ShouldGetAllPrivateParticipantsFromEvent()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();;
        await Testing.AddAsync(new Event()
        {
            Id = guid1,
            Name = "Event",
            Date = DateTime.Now,
            Location = "Location"
        });
        
        await Testing.AddAsync(new PrivateParticipant()
        {
            Id = guid2,
            FirstName = "PrivateParticipant",
            LastName = "PrivateParticipantLast",
            PaymentMethod = PaymentMethod.ByCard,
            IdNumber = "11111111111"

        });

        // await Testing.SendAsync(command);
        await Testing.AddAsync(new EventParticipant()
        {
            EventId = guid1,
            PrivateParticipantId = guid2,
        
        });

        var query = new GetAllEventPrivatePrticipantsQuery()
        {
            Id = guid1
        };

        var result = await Testing.SendAsync(query);

        result.Should().HaveCount(1);
        result.First().FirstName.Should().Be("PrivateParticipant");
        result.First().LastName.Should().Be("PrivateParticipantLast");
        result.First().IdNumber.Should().Be("11111111111");
    }
}
