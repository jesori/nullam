using AutoMapper.QueryableExtensions;

namespace Application.BusinessParticipants.Queries;

public record GetAllBusinessParticipantQuery : IRequest<List<GetBusinessParticipantDto>>;

public class GetAllBusinessParticipantQueryHandler : IRequestHandler<GetAllBusinessParticipantQuery, List<GetBusinessParticipantDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllBusinessParticipantQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetBusinessParticipantDto>> Handle(GetAllBusinessParticipantQuery request, CancellationToken cancellationToken)
    {
        return await _context.BusinessParticipants
            .AsNoTracking()
            .ProjectTo<GetBusinessParticipantDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }
}