using CRUD.App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.App.Infrastructure.Persistence.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public const int MaxNameLength = 200;
    public const int MaxMobilePhoneLength = 50;
    public const int MaxJobTitleLength = 150;
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(MaxNameLength);

        builder.Property(c => c.MobilePhone)
            .IsRequired()
            .HasMaxLength(MaxMobilePhoneLength);

        builder.Property(c => c.JobTitle)
            .IsRequired(false)
            .HasMaxLength(MaxJobTitleLength);

        builder.Property(c => c.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(c => c.CreatedAt)
            .IsRequired();
    }
}