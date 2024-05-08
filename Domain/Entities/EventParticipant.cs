using Domain.Common;

namespace Domain.Entities;

public class EventParticipant : BaseEntity
{
    public Guid EventId { get; set; }
    public Event? Event { get; set; }

    public Guid? PrivateParticipantId { get; set; }
    public PrivateParticipant? PrivateParticipant { get; set; }

    public Guid? BusinessParticipantId { get; set; }
    public BusinessParticipant? BusinessParticipant { get; set; }
}