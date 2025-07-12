using ContactSystem.Application.Dtos;
using ContactSystem.Application.Repositories.Interfaces;
using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services
{
    public class OfficesService : IOfficesService
    {
        public Guid CurrentOfficeId { get; set; }
        private readonly IOfficesRepository _officesRepository;

        public OfficesService(IOfficesRepository officesRepository)
        {
            CurrentOfficeId = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98");
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
