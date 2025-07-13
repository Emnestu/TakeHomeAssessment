using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Services.Interfaces;

public interface IContactsService
{
    Task<ContactEntity?> GetContactByIdAsync(Guid id);
    Task<IEnumerable<ContactEntity>> GetAllContactsAsync();
    Task AddContactAsync(ContactEntity contact);
    Task UpdateContactAsync(ContactEntity contact);
    Task DeleteContactAsync(Guid id);
    Task<PagedResult<ContactDto>> SearchContactsAsync(string? searchTerm, Guid? officeId, int page, int pageSize);
}