using Application.PrivateParticipants.Commands;
using Application.PrivateParticipants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateParticipantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrivateParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPrivateParticipantDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPrivateParticipantQuery()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPrivateParticipantDto>> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetPrivateParticipantByIdQuery(){Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePrivateParticipant([FromBody] CreatePrivateParticipantCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // GET: PrivateParticipantes/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrivateParticipant(Guid id, [FromBody] UpdatePrivateParticipantCommand command)
        {
            if (id != command.Id) return BadRequest("Not valid Id");
            return Ok(await _mediator.Send(command));
        }

        // POST: PrivateParticipantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrivateParticipant(Guid id)
        {
            await _mediator.Send(new DeletePrivateParticipantCommand(id));
            return NoContent();
        }
    }
}