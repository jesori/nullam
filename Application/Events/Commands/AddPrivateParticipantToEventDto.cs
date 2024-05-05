namespace Application.Events.Commands;

public class AddPrivateParticipantToEventDto
{
    public Guid EventId { get; set; }

    public Guid PrivateParticipantId { get; set; }
}