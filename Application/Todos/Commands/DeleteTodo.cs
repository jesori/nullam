using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.Todos.Commands;

public record DeleteTodoCommand(int Id) : IRequest;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTodoCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Todos
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Todos.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
