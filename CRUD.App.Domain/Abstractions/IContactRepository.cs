using CRUD.App.Domain.Models;

namespace CRUD.App.Domain.Abstractions;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Contact> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
    Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default);
    Task DeleteAsync(Contact contact, CancellationToken cancellationToken = default);
}
