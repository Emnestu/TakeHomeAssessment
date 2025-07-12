using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.API.Controllers
{
    [Route("api/v{version:apiVersion}/Offices")]
    [ApiExplorerSettings(GroupName = "Offices")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficesService _officesService;

        public OfficesController(IOfficesService officesService)
        {
            _officesService = officesService;
        }

        /// <summary>
        /// Retrieves a list of all available Offices.
        /// </summary>
        /// <returns>A list of Offices.</returns>
        /// <response code="200">Offices retrieved successfully.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("GetOfficesWithContacts")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<OfficeDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OfficeDto>>>> GetOfficesWithContacts()
        {
            var officeDtos = (await _officesService.GetOfficesWithContactsAsync()).ToList();

            return new ApiResponse<IEnumerable<OfficeDto>>(true, "Offices retrieved successfully", officeDtos, officeDtos.Count);
        }
    }
}