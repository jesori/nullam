using Application.BusinessParticipants.Queries;
using Domain.Entities;
using Domain.Enums;

namespace Tests.BusinessParticipants.Queries;

public class GetBParticipantsTests: BaseTestFixture
{
    [Test]
    public async Task ShouldGetAllBusinessParticipantBusinessParticipants()
    {

        await Testing.AddAsync(new BusinessParticipant()
        {
            Name= "Name",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var query = new GetAllBusinessParticipantQuery();

        var result = await Testing.SendAsync(query);

        result.Should().HaveCount(1);
        result!.First().Name.Should().Be("Name");
        result!.First().IdNumber.Should().Be("1111111111");
        result!.First().PaymentMethod.Should().Be(PaymentMethod.ByCard);
    }

    [Test]
    public async Task ShouldGetBusinessParticipantById()
    {
        var guid = Guid.NewGuid();
        await Testing.AddAsync(new BusinessParticipant()
        {
            Id = guid,
            Name= "Name",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var query = new GetBusinessParticipantByIdQuery()
        {
            Id = guid
        };

        var result = await Testing.SendAsync(query);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Name");
        result!.IdNumber.Should().Be("1111111111");
        result!.PaymentMethod.Should().Be(PaymentMethod.ByCard);
    }
    
}