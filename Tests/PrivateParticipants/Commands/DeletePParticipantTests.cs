using Application.PrivateParticipants.Commands;
using Application.PrivateParticipants.Queries;
using Domain.Entities;
using Domain.Enums;

namespace Tests.PrivateParticipants.Commands
{
    public class DeletePParticipantTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireValidParticipantId()
        {
            var command = new DeletePrivateParticipantCommand(new Guid());
            await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteParticipant()
        {
            var command = new CreatePrivateParticipantCommand()
            {
                FirstName= "PrivateParticipant",
                LastName ="Last",
                IdNumber = "1111111111",
                PaymentMethod = PaymentMethod.ByCard,
                Info = "Info"
            };

            var id = await Testing.SendAsync(command);

            await Testing.SendAsync(new DeletePrivateParticipantCommand(id));
        
            var list = await Testing.FindAsync<PrivateParticipant>(id);
        
            list.Should().BeNull();
        }
    }
}
