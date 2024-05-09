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


        modelBuilder.Entity<EventParticipant>()
            .HasOne(e => e.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(i => i.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(e => e.PrivateParticipant)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(i => i.PrivateParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(e => e.BusinessParticipant)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(i => i.BusinessParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
