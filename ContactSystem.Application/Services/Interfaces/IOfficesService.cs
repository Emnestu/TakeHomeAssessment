using ContactSystem.Application.Dtos;

namespace ContactSystem.Application.Services.Interfaces
{
    public interface IOfficesService
    {
        Guid CurrentOfficeId { get; set; }
        Task<IEnumerable<OfficeDto>> GetOfficesWithContactsAsync();
    }
}
