using CRUD.App.Domain.Abstractions;
using CRUD.App.Domain.Models;
using CRUD.APP.Application.DTOs;
using CRUD.APP.Application.Interfaces;
using FluentValidation;

namespace CRUD.App.Application.Services;

public sealed class ContactService(
    IContactRepository repository,
    IValidator<CreateContactRequest> createValidator,
    IValidator<UpdateContactRequest> updateValidator) : IContactService
{
    public async Task<ContactDto?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var contact = await repository.GetByIdAsync(id, cancellationToken);
        return contact is null ? null : MapToDto(contact);
    }

    public async Task<(IReadOnlyList<ContactDto> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await repository.GetPagedAsync(page, pageSize, cancellationToken);
        var dtos = items.Select(MapToDto).ToList();

        return (dtos, totalCount);
    }

    public async Task<ContactDto> CreateAsync(
        CreateContactRequest request, 
        CancellationToken cancellationToken = default)
    {
        await createValidator.ValidateAndThrowAsync(request, cancellationToken);

        var contact = Contact.Create(
            request.Name,
            request.MobilePhone,
            request.JobTitle,
            request.BirthDate);

        await repository.AddAsync(contact, cancellationToken);

        return MapToDto(contact);
    }

    public async Task UpdateAsync(
        UpdateContactRequest request, 
        CancellationToken cancellationToken = default)
    {
        await updateValidator.ValidateAndThrowAsync(request, cancellationToken);

        var contact = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Contact with ID {request.Id} not found.");

        contact.Update(
            request.Name,
            request.MobilePhone,
            request.JobTitle,
            request.BirthDate);

        await repository.UpdateAsync(contact, cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var contact = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Contact with ID {id} not found.");

        await repository.DeleteAsync(contact, cancellationToken);
    }

    private static ContactDto MapToDto(Contact contact)
    {
        return new ContactDto
        {
            Id = contact.Id,
            Name = contact.Name,
            MobilePhone = contact.MobilePhone,
            JobTitle = contact.JobTitle,
            BirthDate = contact.BirthDate,
            CreatedAt = contact.CreatedAt
        };            
    }
}