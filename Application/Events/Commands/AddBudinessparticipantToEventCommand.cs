using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Events.Commands;

public record AddBusinessParticipantToEventCommand : IRequest<Guid>
{
    public AddBusinessParticipantToEventDto AddParticipantToEventDto{ get; set; }
}
public class AddBusinessParticipantToEventCommandHandler : IRequestHandler<AddBusinessParticipantToEventCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddBusinessParticipantToEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddBusinessParticipantToEventCommand command, CancellationToken cancellationToken)
    {
        EventParticipant eventParticipant = new()
        {
            EventId = command.AddParticipantToEventDto.EventId,
            BusinessParticipantId = command.AddParticipantToEventDto.BusinessParticipantId,
        };

        await _context.EventParticipants.AddAsync(eventParticipant, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return eventParticipant.Id;
    }
}