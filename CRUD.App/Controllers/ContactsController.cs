using CRUD.APP.Application.DTOs;
using CRUD.APP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ContactsController(IContactService contactService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await contactService.GetPagedAsync(
            page, pageSize, cancellationToken);

        return Ok(new 
        { 
            Items = items, 
            TotalCount = totalCount 
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var contact = await contactService.GetByIdAsync(id, cancellationToken);
        if (contact is null)
        {
            return NotFound(new 
            { 
                StatusCode = 404, 
                Message = $"Contact with ID {id} not found." 
            });
        }

        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        var createdContact = await contactService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdContact.Id },
            createdContact);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
        {
            return BadRequest(new 
            { 
                StatusCode = 400, 
                Message = "ID in URL does not match ID in request body." 
            });
        }

        await contactService.UpdateAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await contactService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}