using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common;

public interface IAppDbContext
{
    public DbSet<Todo> Todos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
