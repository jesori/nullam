using Application.Events.Commands;
using Application.PrivateParticipants.Commands;
using Domain.Entities;
using Domain.Enums;

namespace Tests.PrivateParticipants.Commands;

public class UpdatePParticipantTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidEventId()
    {
        var command = new UpdatePrivateParticipantCommand()
            {
                Id = new Guid(),
                FirstName= "PrivateParticipant",
                LastName ="Last",
                IdNumber = "1111111111",
                PaymentMethod = PaymentMethod.ByCard,
                Info = "Info"
            }
            ;
        await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateEvent()
    {
        var id = await Testing.SendAsync(new CreatePrivateParticipantCommand()
        {
            FirstName= "PrivateParticipant",
            LastName ="Last",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var command = new UpdatePrivateParticipantCommand()
        {            
            Id = id,
            FirstName= "PrivateParticipant2",
            LastName ="Last2",
            IdNumber = "1111112222",
            PaymentMethod = PaymentMethod.InCache,
            Info = "Info"
        };

        await Testing.SendAsync(command);

        var result = await Testing.FindAsync<PrivateParticipant>(id);

        result.Should().NotBeNull();
        result!.FirstName.Should().Be(command.FirstName);
        result!.LastName.Should().Be(command.LastName);
        result!.IdNumber.Should().Be(command.IdNumber);
        result!.PaymentMethod.Should().Be(command.PaymentMethod);
    }

}