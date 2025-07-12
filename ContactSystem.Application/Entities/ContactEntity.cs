namespace ContactSystem.Application.Entities;

public class ContactEntity : Entity<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public ICollection<ContactOfficeRelation>? ContactOffices { get; set; } = new List<ContactOfficeRelation>();
}