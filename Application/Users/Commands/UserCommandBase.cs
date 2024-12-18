using MediatR;

namespace Application.Users.Commands
{
    public abstract class UserCommandBase : IRequest<Domain.Entities.User>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
