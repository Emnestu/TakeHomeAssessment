using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Repositories.Interfaces;

public interface IContactsRepository : IEntitiesRepository<ContactEntity, Guid>
{
    Task<IEnumerable<ContactEntity>> GetContactsByNameOrEmailAsync(string? searchTerm, Guid? officeId, int page, int pageSize);
}