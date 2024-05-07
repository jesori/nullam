using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Domain.Enums;

namespace Application.Events.Commands;

public record AddPrivateParticipantToEventCommand : IRequest<Guid>
{
    public AddPrivateParticipantToEventDto AddParticipantToEventDto{ get; set; }

}
public class AddParticipantToEventCommandHandler : IRequestHandler<AddPrivateParticipantToEventCommand, Guid>
{
    private readonly IAppDbContext _context;

    public AddParticipantToEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddPrivateParticipantToEventCommand command, CancellationToken cancellationToken)
    {
        var participant = await _context.PrivateParticipants.FindAsync(command.AddParticipantToEventDto.PrivateParticipantId);
        var ev = await _context.Events.FindAsync(command.AddParticipantToEventDto.EventId);

        Guard.Against.NotFound(command.AddParticipantToEventDto.PrivateParticipantId, participant);
        Guard.Against.NotFound(command.AddParticipantToEventDto.EventId, ev);

        var existingEventParticipant = await _context.EventParticipants
            .FirstOrDefaultAsync(ep =>
                    ep.EventId == command.AddParticipantToEventDto.EventId &&
                    ep.BusinessParticipantId == command.AddParticipantToEventDto.PrivateParticipantId,
                cancellationToken);

        if (existingEventParticipant != null)
        {
            return existingEventParticipant.Id;
        }

        EventParticipant eventParticipant = new()
        {
            EventId = command.AddParticipantToEventDto.EventId,
            PrivateParticipantId = command.AddParticipantToEventDto.PrivateParticipantId,
        };

        await _context.EventParticipants.AddAsync(eventParticipant, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return eventParticipant.Id;
    }
}