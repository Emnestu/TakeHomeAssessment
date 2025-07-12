using ContactSystem.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure;

public class GraniteDataContext : DbContext, IGraniteDataContext
{
    public GraniteDataContext()
    {
    }

    public GraniteDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ContactEntity> Contacts { get; set; } = null!;

    public DbSet<OfficeEntity> Offices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OfficeEntity>();
        
        modelBuilder.Entity<ContactEntity>();

        modelBuilder.Entity<ContactOfficeRelation>()
            .HasKey(x => new { x.ContactId, x.OfficeId });

        modelBuilder.Entity<ContactEntity>()
            .HasMany(x => x.ContactOffices)
            .WithOne(x => x.Contact)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OfficeEntity>()
            .HasMany(x => x.ContactOffices)
            .WithOne(x => x.Office)
            .OnDelete(DeleteBehavior.Cascade);
    }
}