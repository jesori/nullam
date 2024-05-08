using Ardalis.GuardClauses;
using MediatR;
using System;

namespace Application.Events.Commands;

public record UpdateEventCommand : IRequest<Guid>
{
    public Guid Id { get; init; }

    public string Name { get; set; } = default!;

    public DateTime Date { get; set; } = default!;

    public string Location { get; set; } = default!;

    public string? Info { get; set; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Date = request.Date.ToLocalTime();
        entity.Location = request.Location;
        entity.Info = request.Info;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
