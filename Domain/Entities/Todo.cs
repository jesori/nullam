using Domain.Common;

namespace Domain.Entities;

public class Todo : BaseEntity
{
    public string? Title { get; set; }

    public DateTime? DueBy { get; set; }

    public bool IsComplete { get; set; }
}
