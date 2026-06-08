using CRUD.App.Domain.Abstractions;
using CRUD.App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.App.Infrastructure.Persistence.Repositories;

public sealed class ContactRepository(AppDbContext context) : IContactRepository
{

    public async Task<Contact?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await context.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<Contact> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var sanitizedPage = pageNumber < 1 ? 1 : pageNumber;
        var sanitizedSize = pageSize < 1 ? 10 : pageSize;

        var query = context.Contacts.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(c => c.Name) 
            .Skip((sanitizedPage - 1) * sanitizedSize)
            .Take(sanitizedSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task AddAsync(
        Contact contact, 
        CancellationToken cancellationToken = default)
    {
        await context.Contacts.AddAsync(contact, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Contact contact, 
        CancellationToken cancellationToken = default)
    {
        context.Contacts.Update(contact);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Contact contact, 
        CancellationToken cancellationToken = default)
    {
        context.Contacts.Remove(contact);
        await context.SaveChangesAsync(cancellationToken);
    }
}