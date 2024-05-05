using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.PrivateParticipants.Commands;

public record DeletePrivateParticipantCommand(Guid Id) : IRequest;

public class DeletePrivateParticipantCommandHandler : IRequestHandler<DeletePrivateParticipantCommand>
{
    private readonly IAppDbContext _context;

    public DeletePrivateParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePrivateParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PrivateParticipants
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.PrivateParticipants.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
