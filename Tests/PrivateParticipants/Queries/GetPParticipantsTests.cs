using Application.PrivateParticipants.Queries;
using Domain.Entities;
using Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tests.PrivateParticipants.Queries;

public class GetPParticipantsTests: BaseTestFixture
{
    [Test]
    public async Task ShouldGetAllPrivateParticipantPrivateParticipants()
    {

        await Testing.AddAsync(new PrivateParticipant()
        {
            FirstName= "PrivateParticipant",
            LastName ="Last",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var query = new GetAllPrivateParticipantQuery();

        var result = await Testing.SendAsync(query);

        result.Should().HaveCount(1);
        result!.First().FirstName.Should().Be("PrivateParticipant");
        result!.First().LastName.Should().Be("Last");
        result!.First().IdNumber.Should().Be("1111111111");
        result!.First().PaymentMethod.Should().Be(PaymentMethod.ByCard);
    }

    [Test]
    public async Task ShouldGetPrivateParticipantById()
    {
        var guid = Guid.NewGuid();
        await Testing.AddAsync(new PrivateParticipant()
        {
            Id = guid,
            FirstName= "PrivateParticipant",
            LastName ="Last",
            IdNumber = "1111111111",
            PaymentMethod = PaymentMethod.ByCard,
            Info = "Info"
        });

        var query = new GetPrivateParticipantByIdQuery()
        {
            Id = guid
        };

        var result = await Testing.SendAsync(query);

        result.Should().NotBeNull();
        result!.FirstName.Should().Be("PrivateParticipant");
        result!.LastName.Should().Be("Last");
        result!.IdNumber.Should().Be("1111111111");
        result!.PaymentMethod.Should().Be(PaymentMethod.ByCard);
    }
    
}