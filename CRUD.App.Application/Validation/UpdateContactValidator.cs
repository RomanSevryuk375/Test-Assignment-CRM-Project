using CRUD.App.Domain.Models;
using CRUD.APP.Application.DTOs;
using FluentValidation;

namespace CRUD.App.Application.Validation;

public sealed class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Contact.MaxNameLength);

        RuleFor(x => x.MobilePhone)
            .NotEmpty()
            .MaximumLength(Contact.MaxMobilePhoneLength)
            .Matches(@"^\+?[0-9\s-\(\)]+$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.JobTitle)
            .MaximumLength(Contact.MaxJobTitleLength)
            .When(x => !string.IsNullOrEmpty(x.JobTitle));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}