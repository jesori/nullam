using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common;

public interface IAppDbContext
{
    public DbSet<PrivateParticipant> PrivateParticipants { get; }
    public DbSet<BusinessParticipant> BusinessParticipants { get; }
    public DbSet<Event> Events { get; }
    public DbSet<EventParticipant> EventParticipants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
