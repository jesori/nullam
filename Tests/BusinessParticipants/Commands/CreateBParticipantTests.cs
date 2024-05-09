using Application.BusinessParticipants.Commands;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Tests.BusinessParticipants.Commands;

public class CreateBParticipantTests
{
    [Test]
    public async Task ShouldAddParticipant()
    {
        var command = new CreateBusinessParticipantCommand()
        {
            Name = "name",
            PaymentMethod = PaymentMethod.ByCard,
            IdNumber = "11111111111",
            Info = "Info"
        };

        var eventId = await Testing.SendAsync(command);

        var dbEvent = await Testing.FindAsync<BusinessParticipant>(eventId);

        dbEvent.Should().NotBeNull();
        dbEvent?.Name.Should().Be(command.Name);
        dbEvent?.IdNumber.Should().Be(command.IdNumber);
        dbEvent?.PaymentMethod.Should().Be(command.PaymentMethod);
    }

    [Test]
    public async Task ShouldNotAddParticipantWithoutNameAndIdNumber()
    {

        var command = new CreateBusinessParticipantCommand()
        {
            Info = "Info"
        };
        
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<DbUpdateException>();

    
    }
}