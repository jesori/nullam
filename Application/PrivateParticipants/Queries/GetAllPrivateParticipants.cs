using Application.PrivateParticipants.Queries;
using AutoMapper.QueryableExtensions;

namespace Application.PrivateParticipants.Queries;

public record GetAllPrivateParticipantQuery : IRequest<List<GetPrivateParticipantDto>>;

public class GetAllPrivateParticipantQueryHandler : IRequestHandler<GetAllPrivateParticipantQuery, List<GetPrivateParticipantDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPrivateParticipantQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetPrivateParticipantDto>> Handle(GetAllPrivateParticipantQuery request, CancellationToken cancellationToken)
    {
        return await _context.PrivateParticipants
            .AsNoTracking()
            .ProjectTo<GetPrivateParticipantDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.LastName)
            .ToListAsync(cancellationToken);
    }
}