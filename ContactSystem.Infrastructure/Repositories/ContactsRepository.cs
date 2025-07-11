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

    public async Task<(IEnumerable<ContactEntity>, int)> SearchContactsAsync(Guid officeId, string searchTerm, int page, int pageSize)
    {
        var query = _dbSet
            .Where(p => (p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm) || p.Email.Contains(searchTerm)) &&
                        p.ContactOffices.Any(ph => ph.OfficeId == officeId))
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName);

        var totalRecords = await query.CountAsync();
        var Contacts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (Contacts, totalRecords);
    }
}