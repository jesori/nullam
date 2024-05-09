using System;
using System.Threading.Tasks;
using Application.Events.Commands;
using Application.Events.Queries;
using Domain.Entities;
using Domain.Enums;

namespace Tests.Events.Commands
{
    public class DeleteEventTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireValidEventId()
        {
            var command = new DeleteEventCommand(new Guid());
            await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteEventList()
        {
            var command = new CreateEventCommand()
            {
                Name = "Event",
                Date = DateTime.Now,
                Location = "Location",
                Info = "Info"
            };

            var eventId = await Testing.SendAsync(command);

            await Testing.SendAsync(new DeleteEventCommand(eventId));
        
            var list = await Testing.FindAsync<Event>(eventId);
        
            list.Should().BeNull();
        }

        [Test]
        public async Task ShouldRemoveBusinessParticipantFromEvent()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();;
            var guid3 = Guid.NewGuid();;
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

            // await Testing.SendAsync(command);
            await Testing.AddAsync(new EventParticipant()
            {
                Id = guid3,
                EventId = guid1,
                BusinessParticipantId = guid2,
        
            });

            var command = new RemoveParticipantFromEventCommand(guid3);

            await Testing.SendAsync(command);

            var query = new GetAllEventBusinessPrticipantsQuery()
            {
                Id = guid1
            };

            var result = await Testing.SendAsync(query);

            result.Should().HaveCount(0);
        }
    }
}
