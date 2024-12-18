using Application.Contacts.Commands;
using Application.Contacts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("searchId/{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var query = new GetContactByIdQuery { Id = id };
            var contact = await _mediator.Send(query);
            return Ok(contact);
        }

        [HttpGet("searchAll")]
        public async Task<IActionResult> GetContacts()
        {
            var query = new GetContactsQuery();
            var contacts = await _mediator.Send(query);
            return Ok(contacts);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateContact(CreateContactCommand command)
        {
            var createContact = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetContact), new { id = createContact.Id }, createContact);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateContact(int id, UpdateContactCommand command)
        {
            command.Id = id;
            var updateContact = await _mediator.Send(command);
            return updateContact != null ? Ok(updateContact) : NotFound("Contact not found!");
        }

        [HttpDelete("delete/{id}")] 
        public async Task<IActionResult> DeleteContact(int id)
        {
            var command = new DeleteContacttCommand { Id = id };
            var deleteContact = await _mediator.Send(command);

            return deleteContact != null ? Ok(deleteContact) : NotFound("Contact not found!");
        }

    }
}
