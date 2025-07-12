namespace ContactSystem.Application.Dtos;

public class ContactDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IEnumerable<string>? OfficeNames { get; set; } = new List<string>();
}