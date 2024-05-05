using Domain.Common;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class BusinessParticipant : BaseEntity
{
    public string Name { get; set; } = default!;

    public string ParticipantsNumber{ get; set; } = default!;

    public PaymentMethod PaymentMethod { get; set; } = default!;

    [MaxLength(5000)]
    public string? Info { get; set; }

    public ICollection<EventParticipant>? EventParticipants { get; set; }
}
