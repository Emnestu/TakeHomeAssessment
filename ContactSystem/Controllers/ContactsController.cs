using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Services;
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
        private readonly IContactsService _ContactsService;
        private readonly IOfficesService _officeService;

        public ContactsController(IContactsService ContactsService, OfficeService officeService)
        {
            _ContactsService = ContactsService;
            _officeService = officeService;
        }

        /// <summary>
        /// Retrieves a list of Contacts based on the provided search criteria.
        /// </summary>
        /// <param name="name">The name to search for Contacts.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Contacts that match the search criteria.</returns>
        /// <response code="200">Contacts retrieved successfully.</response>
        /// <response code="400">If the search term is empty or invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getContacts")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContactEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetContacts([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<object>(false, "Search term cannot be empty.", null));
            }

            //To Do
            return Ok();
        }
    }
}
