using Ardalis.GuardClauses;
using Domain.Enums;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.BusinessParticipants.Commands;

public record UpdateBusinessParticipantCommand : IRequest<Guid>
{
    public Guid Id { get; init; } = default!;

    public string Name { get; set; } = default!;

    public string IdNumber { get; set; } = default!;

    public string ParticipantsNumber{ get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(5000)]
    public string? Info { get; set; }
}

public class UpdateBusinessParticipantCommandHandler : IRequestHandler<UpdateBusinessParticipantCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateBusinessParticipantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateBusinessParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BusinessParticipants
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.IdNumber = request.IdNumber;
        entity.ParticipantsNumber = request.ParticipantsNumber;
        entity.PaymentMethod = request.PaymentMethod;
        entity.Info = request.Info;
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
