using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.API.Controllers;

[Route("api/v{version:apiVersion}/Contacts")]
[ApiExplorerSettings(GroupName = "Contacts")]
[ApiController]
[ApiVersion("1.0")]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _contactsService;

    public ContactsController(IContactsService contactsService)
    {
        _contactsService = contactsService;
    }

    /// <summary>
    /// Retrieves a list of Contacts based on the provided search criteria.
    /// </summary>
    /// <param name="searchTerm">The name or email to search for Contacts.</param>
    /// <param name="officeId">The GUID of the office to search. If omitted, searches all offices.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of records per page.</param>
    /// <returns>A list of Contacts that match the search criteria.</returns>
    /// <response code="200">Contacts retrieved successfully.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("GetContacts")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContactDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 500)]
    [Produces("application/json")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ContactDto>>>> GetContacts([FromQuery] string? searchTerm, [FromQuery] Guid? officeId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _contactsService.SearchContactsAsync(searchTerm, officeId, page, pageSize);

        return new ApiResponse<IEnumerable<ContactDto>>(true, "Contacts retrieved successfully", pagedResult.Items, pagedResult.TotalCount);
    }
}