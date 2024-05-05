using Application.Events.Queries;
using Ardalis.GuardClauses;

namespace Application.Events.Queries;

public record GetEventByIdQuery : IRequest<GetEventDto>
{
    public Guid Id { get; set; }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetEventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FirstOrDefaultAsync(a => a.Id == request.Id);
        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<GetEventDto>(entity);
    }
}
