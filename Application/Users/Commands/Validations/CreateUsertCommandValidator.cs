using Application.Users.Commands;
using FluentValidation;

namespace Application.User.Commands.ValidationsCommand
{
    public class CreateUsertCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUsertCommandValidator() 
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("The name is mandatory!").Length(4, 250).WithMessage("The o Name must have between 4 and 250 caracters");

            RuleFor(c => c.Password).NotEmpty().WithMessage("The name is mandatory!").Length(8, 12).WithMessage("The o password must have between 8 and 12 caracters");

           
        }
    }
}
