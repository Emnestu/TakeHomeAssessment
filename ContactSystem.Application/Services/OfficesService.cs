using ContactSystem.Application.Dtos;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services
{
    public class OfficesService : IOfficesService
    {
        private readonly IOfficesRepository _officesRepository;

        public OfficesService(IOfficesRepository officesRepository)
        {
            _officesRepository = officesRepository;
        }
        
        public async Task<IEnumerable<OfficeDto>> GetOfficesWithContactsAsync()
        {
            var officeEntities = await _officesRepository.GetOfficesWithContactsAsync();
        
            var officeDtos = officeEntities.Select(c =>
            {
                var officeDto = new OfficeDto()
                {
                    Name = c.Name,
                    OfficeId = c.Id
                };
                
                return officeDto;
            }).ToList();

            return officeDtos;
        }
    }

}
