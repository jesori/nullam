using Application.BusinessParticipants.Commands;
using Domain.Entities;
using Domain.Enums;

namespace Tests.BusinessParticipants.Commands;

public class UpdateBParticipantTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidEventId()
    {
        var command = new UpdateBusinessParticipantCommand()
            {
                Id = new Guid(),
                Name= "Name",
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
        var id = await Testing.SendAsync(new CreateBusinessParticipantCommand()
        {
            Name = "Name",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var command = new UpdateBusinessParticipantCommand()
        {            
            Id = id,
            Name = "Name2",
            IdNumber = "1111112222",
            PaymentMethod = PaymentMethod.InCache,
            Info = "Info"
        };

        await Testing.SendAsync(command);

        var result = await Testing.FindAsync<BusinessParticipant>(id);

        result.Should().NotBeNull();
        result!.Name.Should().Be(command.Name);
        result!.IdNumber.Should().Be(command.IdNumber);
        result!.PaymentMethod.Should().Be(command.PaymentMethod);
    }

}