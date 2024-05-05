using Domain.Common;

namespace Domain.Entities;

public class Event : BaseEntity
{
    public string Name { get; set; } = default!;

    public DateTime Date { get; set; } = default!;

    public string Location { get; set; } = default!;

    public string? Info { get; set; }

    public ICollection<EventParticipant>? EventParticipants { get; set; }

}