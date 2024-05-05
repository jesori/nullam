namespace Application.Events.Commands;

public class AddBusinessParticipantToEventDto
{
    public Guid EventId { get; set; }

    public Guid BusinessParticipantId { get; set; }
}