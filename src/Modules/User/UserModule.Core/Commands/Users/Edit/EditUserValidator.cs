using Common.Application.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.Required);

        RuleFor(u => u.Family)
            .NotEmpty()
            .NotNull().WithMessage(ValidationMessages.Required);
    }
}