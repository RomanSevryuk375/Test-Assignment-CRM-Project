using CRUD.APP.Application.DTOs;


namespace CRUD.APP.Application.Interfaces;

public interface IContactService
{
    Task<ContactDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<ContactDto> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<ContactDto> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateContactRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}