namespace CRUD.App.Domain.Models;

public sealed class Contact
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string MobilePhone { get; private set; } = string.Empty;
    public string? JobTitle { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Contact(
        string name,
        string mobilePhone,
        string? jobTitle,
        DateTime birthDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        MobilePhone = mobilePhone;
        JobTitle = jobTitle;
        BirthDate = birthDate;
        CreatedAt = DateTime.UtcNow;
    }

    public static Contact Create(string name, string mobilePhone, string? jobTitle, DateTime birthDate)
    {
        var validated = NormalizeAndValidate(name, mobilePhone, jobTitle, birthDate);

        return new Contact(
            validated.Name,
            validated.Phone,
            validated.JobTitle,
            validated.BirthDate);
    }

    public void Update(string name, string mobilePhone, string? jobTitle, DateTime birthDate)
    {
        var validated = NormalizeAndValidate(name, mobilePhone, jobTitle, birthDate);

        Name = validated.Name;
        MobilePhone = validated.Phone;
        JobTitle = validated.JobTitle;
        BirthDate = validated.BirthDate;
    }

    private static (string Name, string Phone, string? JobTitle, DateTime BirthDate) NormalizeAndValidate(
        string name,
        string mobilePhone,
        string? jobTitle,
        DateTime birthDate)
    {
        var normalizedName = name?.Trim() ?? string.Empty;
        var normalizedPhone = mobilePhone?.Trim() ?? string.Empty;
        var normalizedJobTitle = jobTitle?.Trim();
        var normalizedBirthDate = birthDate.Date;

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(normalizedPhone))
        {
            throw new ArgumentException("Mobile phone cannot be empty or whitespace.", nameof(mobilePhone));
        }

        if (normalizedBirthDate > DateTime.UtcNow.Date)
        {
            throw new ArgumentException("Birth date cannot be in the future.", nameof(birthDate));
        }

        return (normalizedName, normalizedPhone, normalizedJobTitle, normalizedBirthDate);
    }
}