using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Application.Todos.Queries;

public record GetTodoByIdQuery : IRequest<GetTodoDto>
{
    public int Id { get; set; }
}

public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, GetTodoDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  _context.Todos.FirstOrDefault(a => a.Id == request.Id);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<GetTodoDto>(entity);
    }
}
