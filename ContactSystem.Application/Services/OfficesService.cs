using ContactSystem.Application.Services.Interfaces;

namespace ContactSystem.Application.Services
{
    public class OfficesService : IOfficesService
    {
        public Guid CurrentOfficeId { get; set; }

        public OfficesService()
        {
            CurrentOfficeId = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98");
        }
    }

}
