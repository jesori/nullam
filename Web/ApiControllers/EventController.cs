using Application.BusinessParticipants.Queries;
using Application.Events.Commands;
using Application.Events.Queries;
using Application.Events.Commands;
using Application.Events.Queries;
using Application.PrivateParticipants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetEventDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllEventQuery()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEventDto>> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetEventByIdQuery{Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEvent([FromBody] CreateEventCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // GET: Eventes/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
        {
            if (id != command.Id) return BadRequest("Not valid Id");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            await _mediator.Send(new DeleteEventCommand(id));
            return NoContent();
        }

        [HttpPost("addBusiness")]
        public async Task<ActionResult> AddBusinessParticipant([FromBody] AddBusinessParticipantToEventDto businessParticipantToEventDto)
        {
            return Ok(await _mediator.Send(new AddBusinessParticipantToEventCommand
            {
                AddParticipantToEventDto = businessParticipantToEventDto
            }));
        }

        [HttpPost("addPrivate")]
        public async Task<ActionResult> AddPrivateParticipant([FromBody] AddPrivateParticipantToEventDto privateParticipantToEventDto)
        {
            return Ok(await _mediator.Send(new AddPrivateParticipantToEventCommand
            {
                AddParticipantToEventDto = privateParticipantToEventDto
            }));
        }

        [HttpGet("{id}/getAllBusiness")]
        public async Task<ActionResult<List<GetBusinessParticipantDto>>> AddBusinessarticipant(Guid id)
        {
            var participants = await _mediator.Send(new GetAllEventBusinessPrticipantsQuery()
            {
                Id = id
            });

            return Ok(participants);
        }

        [HttpGet("{id}/getAllPrivate")]
        public async Task<ActionResult<List<GetPrivateParticipantDto>>> AddPrivateParticipant(Guid id)
        {
            var participants = await _mediator.Send(new GetAllEventPrivatePrticipantsQuery()
            {
                Id = id
            });

            return  Ok(participants);
        }
    }
}