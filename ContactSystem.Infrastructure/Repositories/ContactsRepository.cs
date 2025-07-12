using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Repositories;

public class ContactsRepository : EntityRepository<ContactEntity, Guid>, IContactsRepository
{
    private readonly GraniteDataContext _context;

    public ContactsRepository(GraniteDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactEntity>> GetContactsByNameAsync(string searchTerm, Guid? officeId, int page, int pageSize)
    {
        var query = _dbSet.Include(p => p.ContactOffices)!
            .ThenInclude(cor => cor.Office)
            .Where(p =>
                p.FirstName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                p.LastName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                p.Email.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));

        if (officeId.HasValue)
        {
            query = query.Where(p => p.ContactOffices!.Any(ph => ph.OfficeId == officeId));
        }

        var contacts = await query
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return contacts;
    }
}