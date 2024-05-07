using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.BusinessParticipants.Queries;

public class GetBusinessParticipantDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; } 

    public string? IdNumber { get; set; } 
    
    public string? ParticipantsNumber { get; set; }

    public PaymentMethod PaymentMethod { get; set; } 

    [MaxLength(5000)]
    public string? Info { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BusinessParticipant, GetBusinessParticipantDto>();
        }
    }
}

