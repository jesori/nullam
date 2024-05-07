using Application.BusinessParticipants.Queries;
using AutoMapper.QueryableExtensions;

namespace Application.Events.Queries;

public record GetAllEventParticipantsQuery : IRequest<List<GetAllParticipantsDto>>
{
    public Guid Id { get; set; }
}

public class GetAllEventParticipantsQueryHandler : IRequestHandler<GetAllEventParticipantsQuery, List<GetAllParticipantsDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEventParticipantsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllParticipantsDto>> Handle(GetAllEventParticipantsQuery request, CancellationToken cancellationToken)
    {
        var a = await _context.EventParticipants
            // .Include(eg => eg.Event)
            // .Include(eg => eg.BusinessParticipant)
            .Where(eg => eg.EventId == request.Id)
            .ProjectTo<GetAllParticipantsDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        // var eventParticipants = await _context.EventParticipants
            // .Include(eg => eg.Event)
            // .Include(eg => eg.BusinessParticipant)
            // .Where(eg => eg.EventId == request.Id && eg.BusinessParticipantId != null)
            // .Select(ep => ep.BusinessParticipant)
            // .ProjectTo<GetBusinessParticipantDto>(_mapper.ConfigurationProvider)
            // .ToListAsync();

        return a;
    }
}