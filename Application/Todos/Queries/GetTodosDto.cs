namespace Application.Todos.Queries;

public class GetTodosDto
{
    public IReadOnlyCollection<GetTodoDto> Todos { get; init; } = Array.Empty<GetTodoDto>();
}
