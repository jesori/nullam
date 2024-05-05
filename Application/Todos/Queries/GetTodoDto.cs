using AutoMapper;

namespace Application.Todos.Queries;

public class GetTodoDto
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public DateTime? DueBy { get; set; }

    public bool IsComplete { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Todo, GetTodoDto>();
        }
    }
}

