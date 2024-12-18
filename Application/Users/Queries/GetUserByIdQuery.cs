using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Domain.Entities.User>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.Entities.User>
        {
            private readonly IUserDapperRepository _UserDapperRepository;

            public GetUserByIdQueryHandler(IUserDapperRepository UserDapperRepository)
            {
                _UserDapperRepository = UserDapperRepository;
            }

            public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellation)
            {
                var User = await _UserDapperRepository.GetUserById(request.Id);
                return User;
            }
        }
    }
}
