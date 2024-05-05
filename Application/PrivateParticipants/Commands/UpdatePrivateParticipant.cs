using Ardalis.GuardClauses;
using Domain.Enums;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.PrivateParticipants.Commands;

public record UpdatePrivateParticipantCommand : IRequest<Guid>
{
    public Guid Id { get; init; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    [Length(11, 11)] public string IdNumber { get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(5000)]
    public string? Info { get; set; }
}

public class UpdatePrivateParticipantCommandHandler : IRequestHandler<UpdatePrivateParticipantCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdatePrivateParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdatePrivateParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PrivateParticipants
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.IdNumber = request.IdNumber;
        entity.PaymentMethod = request.PaymentMethod;
        entity.Info = request.Info;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
