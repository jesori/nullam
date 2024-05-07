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
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.PrivateParticipant)
            .WithMany()
            .HasForeignKey(ep => ep.PrivateParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.BusinessParticipant)
            .WithMany()
            .HasForeignKey(ep => ep.BusinessParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>()
            .HasMany(e => e.EventParticipants)
            .WithOne(ep => ep.Event)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PrivateParticipant>()
            .HasMany(pp => pp.EventParticipants)
            .WithOne(ep => ep.PrivateParticipant)
            .HasForeignKey(ep => ep.PrivateParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BusinessParticipant>()
            .HasMany(bp => bp.EventParticipants)
            .WithOne(ep => ep.BusinessParticipant)
            .HasForeignKey(ep => ep.BusinessParticipantId)
            .OnDelete(DeleteBehavior.Cascade); 

        base.OnModelCreating(modelBuilder);
    }
}
