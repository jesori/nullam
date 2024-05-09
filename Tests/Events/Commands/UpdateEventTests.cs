using Application.Events.Commands;
using Domain.Entities;
using NUnit.Framework.Interfaces;

namespace Tests.Events.Commands;

public class UpdateEventTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidEventId()
    {
        var command = new UpdateEventCommand()
            {
                Id = new Guid(),
                Name = "New Title",
                Date = DateTime.Now,
                Location = "Location",
            }
            ;
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateEvent()
    {
        var listId = await Testing.SendAsync(new CreateEventCommand()
        {
            Name = "New Title",
            Date = DateTime.Now,
            Location = "Location"
        });

        var command = new UpdateEventCommand()
        {
            Id = listId,
            Name = "New Title 2",
            Date = DateTime.Now,
            Location = "Location 2",
        };

        await Testing.SendAsync(command);

        var list = await Testing.FindAsync<Event>(listId);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.Name);
        list!.Location.Should().Be(command.Location);
    }

}