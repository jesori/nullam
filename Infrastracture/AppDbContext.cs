using System.Reflection;
using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<PrivateParticipant> PrivateParticipants => Set<PrivateParticipant>();
    public DbSet<BusinessParticipant> BusinessParticipants => Set<BusinessParticipant>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Event>()
            .HasMany(e => e.EventParticipants)
            .WithOne(c => c.Event)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PrivateParticipant>()
            .HasMany(e => e.EventParticipants)
            .WithOne(c => c.PrivateParticipant)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BusinessParticipant>()
            .HasMany(e => e.EventParticipants)
            .WithOne(c => c.BusinessParticipant)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
