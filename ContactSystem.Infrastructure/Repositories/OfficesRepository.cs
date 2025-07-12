using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Repositories;

public class OfficesRepository : EntityRepository<OfficeEntity, Guid>, IOfficesRepository
{
    public OfficesRepository(GraniteDataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OfficeEntity>> GetOfficesWithContactsAsync()
    {
        return await _dbSet
            .OrderBy(o => o.Name)
            .Where(o => o.ContactOffices!.Any())
            .ToListAsync();
    }
}