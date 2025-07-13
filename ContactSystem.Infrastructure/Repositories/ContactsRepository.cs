using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Repositories;

public class ContactsRepository : EntityRepository<ContactEntity, Guid>, IContactsRepository
{
    public ContactsRepository(GraniteDataContext context) : base(context)
    {
    }

    public async Task<PagedResult<ContactEntity>> GetContactsByNameOrEmailAsync(string? searchTerm, Guid? officeId, int page, int pageSize)
    {
        var query = _dbSet.Include(p => p.ContactOffices)!
            .ThenInclude(cor => cor.Office)
            .AsQueryable();
            
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                ((p.FirstName ?? "") + " " + (p.LastName ?? "")).ToLower().Contains(searchTerm.ToLower()) ||
                p.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        if (officeId.HasValue)
        {
            query = query.Where(p => p.ContactOffices!.Any(ph => ph.OfficeId == officeId));
        }

        var totalCount = await query.CountAsync();

        var contacts = await query
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ContactEntity>
        {
            Items = contacts,
            TotalCount = totalCount
        };
    }
}