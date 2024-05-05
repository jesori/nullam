using Application.BusinessParticipants.Commands;
using Application.BusinessParticipants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessParticipantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetBusinessParticipantDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllBusinessParticipantQuery()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBusinessParticipantDto>> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetBusinessParticipantByIdQuery(){Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBusinessParticipant([FromBody] CreateBusinessParticipantCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // GET: BusinessParticipantes/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBusinessParticipant(Guid id, [FromBody] UpdateBusinessParticipantCommand command)
        {
            if (id != command.Id) return BadRequest("Not valid Id");
            return Ok(await _mediator.Send(command));
        }

        // POST: BusinessParticipantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusinessParticipant(Guid id)
        {
            await _mediator.Send(new DeleteBusinessParticipantCommand(id));
            return NoContent();
        }
    }
}