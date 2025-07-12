using ContactSystem.Application.Dtos;

namespace ContactSystem.Application.Services.Interfaces
{
    public interface IOfficesService
    {
        Task<IEnumerable<OfficeDto>> GetOfficesWithContactsAsync();
    }
}
