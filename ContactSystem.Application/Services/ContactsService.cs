using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services;

public class ContactsService : IContactsService
{
    private readonly IContactsRepository _contactsRepository;

    public ContactsService(IContactsRepository contactsRepository)
    {
        _contactsRepository = contactsRepository;
    }

    public async Task<ContactEntity?> GetContactByIdAsync(Guid id)
    {
        return await _contactsRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ContactEntity>> GetAllContactsAsync()
    {
        return await _contactsRepository.GetAllAsync();
    }

    public async Task AddContactAsync(ContactEntity contact)
    {
        await _contactsRepository.AddAsync(contact);
    }

    public async Task UpdateContactAsync(ContactEntity contact)
    {
        await _contactsRepository.UpdateAsync(contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        await _contactsRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ContactDto>> SearchContactsAsync(string searchTerm, Guid? officeId, int page, int pageSize)
    {
        var contactEntities = await _contactsRepository.GetContactsByNameOrEmailAsync(searchTerm, officeId, page, pageSize);
        
        var contactDtos = contactEntities.Select(c =>
        {
            var contactDto = new ContactDto
            {
                Name = $"{c.FirstName} {c.LastName}",
                Email = c.Email
            };

            if (c.ContactOffices != null)
            {
                contactDto.OfficeNames = c.ContactOffices.Select(cor => cor.Office.Name);
            }

            return contactDto;
        }).ToList();

        return contactDtos;
    }
}
