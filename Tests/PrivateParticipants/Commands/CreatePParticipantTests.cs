using Application.Events.Commands;
using Application.PrivateParticipants.Commands;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Tests.PrivateParticipants.Commands;

public class CreatePParticipantTests
{
    [Test]
    public async Task ShouldAddParticipant()
    {
        var command = new CreatePrivateParticipantCommand()
        {
            FirstName = "First",
            LastName = "Last",
            PaymentMethod = PaymentMethod.ByCard,
            IdNumber = "11111111111",
            Info = "Info"
        };

        var eventId = await Testing.SendAsync(command);

        var dbEvent = await Testing.FindAsync<PrivateParticipant>(eventId);

        dbEvent.Should().NotBeNull();
        dbEvent?.FirstName.Should().Be(command.FirstName);
        dbEvent?.LastName.Should().Be(command.LastName);
        dbEvent?.IdNumber.Should().Be(command.IdNumber);
        dbEvent?.PaymentMethod.Should().Be(command.PaymentMethod);
    }

    [Test]
    public async Task ShouldNotAddParticipantWithoutNameAndIdNumber()
    {

        var command = new CreatePrivateParticipantCommand()
        {
            Info = "Info"
        };
        
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<DbUpdateException>();

    
    }
}