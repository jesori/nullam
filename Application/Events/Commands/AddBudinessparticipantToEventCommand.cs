using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
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
        var participant = await _context.BusinessParticipants.FindAsync(command.AddParticipantToEventDto.BusinessParticipantId);
        var ev = await _context.Events.FindAsync(command.AddParticipantToEventDto.EventId);

        Guard.Against.NotFound(command.AddParticipantToEventDto.BusinessParticipantId, participant);
        Guard.Against.NotFound(command.AddParticipantToEventDto.EventId, ev);

        var existingEventParticipant = await _context.EventParticipants
            .FirstOrDefaultAsync(ep =>
                    ep.EventId == command.AddParticipantToEventDto.EventId &&
                    ep.BusinessParticipantId == command.AddParticipantToEventDto.BusinessParticipantId,
                cancellationToken);

        if (existingEventParticipant != null)
        {
            return existingEventParticipant.Id;
        }

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