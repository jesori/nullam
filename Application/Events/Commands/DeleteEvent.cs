using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.Events.Commands;

public record DeleteEventCommand(Guid Id) : IRequest;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IAppDbContext _context;

    public DeleteEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Events.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
