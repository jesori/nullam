using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Events.Commands;

public record CreateEventCommand : IRequest<Guid>
{
    public string Name { get; set; } = default!;

    public DateTime Date { get; set; } = default!;

    public string Location { get; set; } = default!;

    public string? Info { get; set; }
}
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateEventCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        Event Event = new()
        {
            Name = command.Name,
            Date = command.Date.ToLocalTime(),
            Location = command.Location,
            Info = command.Info
        };

        await _context.Events.AddAsync(Event, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Event.Id;
    }
}
