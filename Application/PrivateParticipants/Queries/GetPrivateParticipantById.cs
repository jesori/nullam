using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Application.PrivateParticipants.Queries;

public record GetPrivateParticipantByIdQuery : IRequest<GetPrivateParticipantDto>
{
    public Guid Id { get; set; }
}

public class GetPrivateParticipantByIdQueryHandler : IRequestHandler<GetPrivateParticipantByIdQuery, GetPrivateParticipantDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetPrivateParticipantByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetPrivateParticipantDto> Handle(GetPrivateParticipantByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  _context.PrivateParticipants.FirstOrDefault(a => a.Id == request.Id);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<GetPrivateParticipantDto>(entity);
    }
}
