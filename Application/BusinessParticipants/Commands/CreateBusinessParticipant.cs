using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.BusinessParticipants.Commands;

public record CreateBusinessParticipantCommand : IRequest<Guid>
{
    public string Name { get; set; } = default!;

    public string ParticipantsNumber{ get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(5000)]
    public string? Info { get; set; }
}

public class CreateBusinessParticipantCommandHandler : IRequestHandler<CreateBusinessParticipantCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateBusinessParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBusinessParticipantCommand command, CancellationToken cancellationToken)
    {
        BusinessParticipant businessParticipant = new()
        {
            Name = command.Name,
            ParticipantsNumber = command.ParticipantsNumber,
            PaymentMethod = command.PaymentMethod,
            Info = command.Info
        };

        await _context.BusinessParticipants.AddAsync(businessParticipant, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return businessParticipant.Id;
    }
}
