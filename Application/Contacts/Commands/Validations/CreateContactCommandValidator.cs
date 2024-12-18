using FluentValidation;

namespace Application.Contacts.Commands.ValidationsCommand
{
    public class CreateUsertCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateUsertCommandValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("The name is mandatory!").Length(4, 250).WithMessage("The o Name must have between 4 and 250 caracters");

            RuleFor(c => c.Email).NotEmpty().EmailAddress();

            RuleFor(c => c.Phone).NotEmpty().WithMessage("The phone is mandatory!").Length(11).WithMessage("the phone field, limit is 11 characters");

            RuleFor(c => c.IsActive).NotNull();
        }
    }
}
