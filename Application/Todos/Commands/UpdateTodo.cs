using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.Todos.Commands;

public record UpdateTodoCommand : IRequest<int>
{
    public int Id { get; init; }

    public string? Title { get; set; }

    public DateTime? DueBy { get; set; }

    public bool IsComplete { get; set; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateTodoCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Todos
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.DueBy = request.DueBy;
        entity.IsComplete = request.IsComplete;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
