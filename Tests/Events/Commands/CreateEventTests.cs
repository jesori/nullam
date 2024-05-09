using Application.Events.Commands;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.Events.Commands;

public class CreateEventTests : BaseTestFixture
{
    [Test]
    public async Task ShouldAddEvent()
    {
        var command = new CreateEventCommand()
        {
            Name = "Event",
            Date = DateTime.Now,
            Location = "Location",
            Info = "Info"
        };

        var eventId = await Testing.SendAsync(command);

        var dbEvent = await Testing.FindAsync<Event>(eventId);

        dbEvent.Should().NotBeNull();
        dbEvent?.Name.Should().Be(command.Name);
        dbEvent?.Date.Should().BeCloseTo(command.Date, TimeSpan.FromSeconds(1));
        dbEvent?.Location.Should().Be(command.Location);
        dbEvent?.Info.Should().Be(command.Info);
    }

    [Test]
    public async Task ShouldNotAddEventWithoutNameAndDate()
    {
        var command = new CreateEventCommand()
        {
            Location = "Location",
            Info = "Info"
        };
        
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<DbUpdateException>();
    }
}