using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Application.BusinessParticipants.Queries;

public record GetBusinessParticipantByIdQuery : IRequest<GetBusinessParticipantDto>
{
    public Guid Id { get; set; }
}

public class GetBusinessParticipantByIdQueryHandler : IRequestHandler<GetBusinessParticipantByIdQuery, GetBusinessParticipantDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetBusinessParticipantByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetBusinessParticipantDto> Handle(GetBusinessParticipantByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  _context.BusinessParticipants.FirstOrDefault(a => a.Id == request.Id);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<GetBusinessParticipantDto>(entity);
    }
}
