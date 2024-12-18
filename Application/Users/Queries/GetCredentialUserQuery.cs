using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries
{
    public class GetCredentialUserQuery : IRequest<Domain.Entities.User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetCredentialUserQuery, Domain.Entities.User>
        {
            private readonly IUserDapperRepository _UserDapperRepository;

            public GetUserByIdQueryHandler(IUserDapperRepository UserDapperRepository)
            {
                _UserDapperRepository = UserDapperRepository;
            }

            public async Task<Domain.Entities.User> Handle(GetCredentialUserQuery request, CancellationToken cancellation)
            {
                var User = await _UserDapperRepository.GetCredentialsUser(request.Username, request.Password);
                return User;
            }
        }
    }
}
