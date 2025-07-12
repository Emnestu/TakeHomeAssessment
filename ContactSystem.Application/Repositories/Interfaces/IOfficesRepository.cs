using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Repositories.Interfaces
{
    public interface IOfficesRepository : IEntitiesRepository<OfficeEntity, Guid>
    {
        Task<IEnumerable<OfficeEntity>> GetOfficesWithContactsAsync();
    }
}