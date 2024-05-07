using System.ComponentModel.DataAnnotations;
using Application.BusinessParticipants.Queries;
using Domain.Enums;

namespace Application.Events.Queries;

public class GetAllParticipantsDto
{
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid? PrivateParticipantId { get; set; }
        public PrivateParticipant? PrivateParticipant { get; set; }

        public Guid? BusinessParticipantId { get; set; }
        public BusinessParticipant? BusinessParticipant { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<EventParticipant, GetAllParticipantsDto>();
            }
        }
}