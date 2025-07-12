namespace ContactSystem.Application.Entities;

public class OfficeEntity : Entity<Guid>
{
    public string Name { get; set; } = null!;

    public ICollection<ContactOfficeRelation>? ContactOffices { get; set; } = new List<ContactOfficeRelation>();
}