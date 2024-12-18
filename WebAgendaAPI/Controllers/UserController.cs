using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("searchId/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var User = await _mediator.Send(query);
            return Ok(User);
        }

        [HttpGet("searchAll")]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var Users = await _mediator.Send(query);
            return Ok(Users);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var createUser = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = createUser.Id }, createUser);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        {
            command.Id = id;
            var updateUser = await _mediator.Send(command);
            return updateUser != null ? Ok(updateUser) : NotFound("User not found!!");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            var deleteUser = await _mediator.Send(command);

            return deleteUser != null ? Ok(deleteUser) : NotFound("User not found!");
        }

    }
}
