using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Repositories;

public class OfficesRepository : EntityRepository<OfficeEntity, Guid>, IOfficesRepository
{
    private readonly GraniteDataContext _context;

    public OfficesRepository(GraniteDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OfficeEntity>> GetOfficesWithContactsAsync()
    {
        return await _dbSet
            .OrderBy(o => o.Name)
            .Where(o => o.ContactOffices!.Any())
            .ToListAsync();
    }
}