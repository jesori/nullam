using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.PrivateParticipants.Commands;

public record CreatePrivateParticipantCommand : IRequest<Guid>
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    [Length(11, 11)] public string IdNumber { get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(5000)]
    public string? Info { get; set; }
}
public class CreatePrivateParticipantCommandHandler : IRequestHandler<CreatePrivateParticipantCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreatePrivateParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePrivateParticipantCommand command, CancellationToken cancellationToken)
    {
        PrivateParticipant PrivateParticipant = new()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            IdNumber = command.IdNumber,
            PaymentMethod = command.PaymentMethod,
            Info = command.Info
        };

        await _context.PrivateParticipants.AddAsync(PrivateParticipant, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return PrivateParticipant.Id;
    }
}
