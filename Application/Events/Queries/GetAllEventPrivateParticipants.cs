using Application.PrivateParticipants.Queries;
using AutoMapper.QueryableExtensions;

namespace Application.Events.Queries;

public record GetAllEventPrivatePrticipantsQuery : IRequest<List<GetPrivateParticipantDto>>
{
    public Guid Id { get; set; }
}

public class GetAllEventPrivatePrticipantsQueryHandler : IRequestHandler<GetAllEventPrivatePrticipantsQuery, List<GetPrivateParticipantDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEventPrivatePrticipantsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetPrivateParticipantDto>> Handle(GetAllEventPrivatePrticipantsQuery request, CancellationToken cancellationToken)
    {
        var eventParticipants = await _context.EventParticipants
            .Include(eg => eg.Event)
            .Include(eg => eg.PrivateParticipant)
            .Where(eg => eg.EventId == request.Id && eg.PrivateParticipantId != null)
            .Select(ep => ep.PrivateParticipant)
            .ProjectTo<GetPrivateParticipantDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return eventParticipants;
    }
}