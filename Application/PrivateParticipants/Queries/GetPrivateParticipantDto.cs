using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.PrivateParticipants.Queries;

public class GetPrivateParticipantDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    [Length(11, 11)] 
    public string IdNumber { get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(1500)]
    public string? Info { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PrivateParticipant, GetPrivateParticipantDto>();
        }
    }
}

