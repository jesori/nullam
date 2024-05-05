using AutoMapper.QueryableExtensions;

namespace Application.Events.Queries;

public record GetAllEventQuery : IRequest<List<GetEventDto>>;

public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, List<GetEventDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEventQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetEventDto>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
    {
        return await _context.Events
            .AsNoTracking()
            .ProjectTo<GetEventDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }
}