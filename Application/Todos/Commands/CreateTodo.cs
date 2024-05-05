using MediatR;

namespace Application.Todos.Commands;

public record CreateTodoCommand : IRequest<int>
{
    public int Id { get; init; }

    public string? Title { get; set; }

    public DateTime? DueBy { get; set; }

    public bool IsComplete { get; set; }
}
public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateTodoCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        Todo todo = new() { Title = command.Title, DueBy = command.DueBy, IsComplete = command.IsComplete};

        await _context.Todos.AddAsync(todo, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}
