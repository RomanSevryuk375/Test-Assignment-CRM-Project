namespace CRUD.APP.Application.DTOs;

public sealed record ContactDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string MobilePhone { get; init; } = string.Empty;
    public string? JobTitle { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; }
    public DateTime CreatedAt { get; init; }
}
