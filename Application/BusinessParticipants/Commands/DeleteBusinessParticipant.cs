using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.BusinessParticipants.Commands;

public record DeleteBusinessParticipantCommand(Guid Id) : IRequest;

public class DeleteBusinessParticipantCommandHandler : IRequestHandler<DeleteBusinessParticipantCommand>
{
    private readonly IAppDbContext _context;

    public DeleteBusinessParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBusinessParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BusinessParticipants
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BusinessParticipants.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
