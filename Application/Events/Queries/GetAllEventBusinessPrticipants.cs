using Application.BusinessParticipants.Queries;
using Application.BusinessParticipants.Queries;
using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Events.Queries;

public record GetAllEventBusinessPrticipantsQuery : IRequest<List<GetBusinessParticipantDto>>
{
    public Guid Id { get; set; }
}

public class GetAllEventBusinessParticipantsQueryHandler : IRequestHandler<GetAllEventBusinessPrticipantsQuery, List<GetBusinessParticipantDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEventBusinessParticipantsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetBusinessParticipantDto>> Handle(GetAllEventBusinessPrticipantsQuery request, CancellationToken cancellationToken)
    {
        var a = await _context.EventParticipants
            .Include(eg => eg.Event)
            .Include(eg => eg.BusinessParticipant)
            .Where(eg => eg.EventId == request.Id).ToListAsync();
        var eventParticipants = await _context.EventParticipants
            .Include(eg => eg.Event)
            .Include(eg => eg.BusinessParticipant)
            .Where(eg => eg.EventId == request.Id && eg.BusinessParticipantId != null)
            .Select(ep => ep.BusinessParticipant)
            .ProjectTo<GetBusinessParticipantDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return eventParticipants;
    }
}