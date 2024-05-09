using Application.BusinessParticipants.Commands;
using Domain.Entities;
using Domain.Enums;

namespace Tests.BusinessParticipants.Commands
{
    public class DeleteBParticipantTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireValidParticipantId()
        {
            var command = new DeleteBusinessParticipantCommand(new Guid());
            await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteParticipant()
        {
            var command = new CreateBusinessParticipantCommand()
            {
                Name= "Name",
                IdNumber = "1111111111",
                PaymentMethod = PaymentMethod.ByCard,
                Info = "Info"
            };

            var id = await Testing.SendAsync(command);

            await Testing.SendAsync(new DeleteBusinessParticipantCommand(id));
        
            var list = await Testing.FindAsync<BusinessParticipant>(id);
        
            list.Should().BeNull();
        }
    }
}
