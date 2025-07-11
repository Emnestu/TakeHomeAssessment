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

        modelBuilder.Entity<OfficeEntity>()
            .HasData(
                new OfficeEntity
                {
                    Id = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98"),
                    Name = "Default Office"
                }
            );

        modelBuilder.Entity<ContactEntity>();

        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity
                {
                    Id = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                    FirstName = "Alejandro",
                    LastName = "Alfonso",
                    Email = "aalfonso@gmail.com"
                }
                , new ContactEntity
                {
                    Id = new Guid("1ec2d3f7-8aa8-4bf5-91b8-045378919049"),
                    FirstName = "Mark",
                    LastName = "Costello",
                    Email = "mcostello@gmail.com"
                }
            );

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

        modelBuilder.Entity<ContactOfficeRelation>()
            .HasData(
                new ContactOfficeRelation
                {
                    ContactId = new Guid("c00b9ff3-b1b6-42fe-8b5a-4c28408fb64a"),
                    OfficeId = new Guid("9ca78c33-4590-43c1-a7c4-55696a5efd44"),
                });
    }
}