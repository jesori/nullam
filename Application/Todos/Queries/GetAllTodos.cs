using AutoMapper.QueryableExtensions;

namespace Application.Todos.Queries;

public record GetAllTodoQuery : IRequest<List<GetTodoDto>>;

public class GetAllTodoQueryHandler : IRequestHandler<GetAllTodoQuery, List<GetTodoDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTodoQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetTodoDto>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        return await _context.Todos
            .AsNoTracking()
            .ProjectTo<GetTodoDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Title)
            .ToListAsync(cancellationToken);
    }
}