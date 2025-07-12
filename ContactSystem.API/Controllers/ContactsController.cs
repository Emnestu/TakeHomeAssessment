using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.API.Controllers
{
    [Route("api/v{version:apiVersion}/Contacts")]
    [ApiExplorerSettings(GroupName = "Contacts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        private readonly IOfficesService _officesService;

        public ContactsController(IContactsService contactsService, IOfficesService officesService)
        {
            _contactsService = contactsService;
            _officesService = officesService;
        }

        /// <summary>
        /// Retrieves a list of Contacts based on the provided search criteria.
        /// </summary>
        /// <param name="name">The name to search for Contacts.</param>
        /// <param name="officeId">The GUID of the office to search. If omitted, searches all offices.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Contacts that match the search criteria.</returns>
        /// <response code="200">Contacts retrieved successfully.</response>
        /// <response code="400">If the search term is empty or invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("GetContacts")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContactDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ContactDto>>>> GetContacts([FromQuery] string name, [FromQuery] Guid? officeId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<IEnumerable<ContactDto>>(false, "Search term cannot be empty", null));
            }

            var contactDtos = (await _contactsService.SearchContactsAsync(name, officeId, page, pageSize)).ToList();

            return new ApiResponse<IEnumerable<ContactDto>>(true, "Contacts retrieved successfully",  contactDtos, contactDtos.Count);
        }
    }
}
