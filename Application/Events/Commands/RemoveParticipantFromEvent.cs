using Ardalis.GuardClauses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Events.Commands;

public record RemoveParticipantFromEventCommand(Guid Id) : IRequest<int>;

public class RemoveParticipantFromEventCommandHandler : IRequestHandler<RemoveParticipantFromEventCommand, int>
{
    private readonly IAppDbContext _context;

    public RemoveParticipantFromEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(RemoveParticipantFromEventCommand request, CancellationToken cancellationToken)
    {
        var affected = await _context.EventParticipants
            .Where(l => l.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        return affected;
    }
}