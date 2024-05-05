using Application.Todos.Commands;
using Application.Todos.Queries;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetTodoDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTodoQuery()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTodoDto>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetTodoByIdQuery(){Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTodo([FromBody] CreateTodoCommand command)
        {
           return Ok(await _mediator.Send(command));
        }

        // GET: Todoes/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] UpdateTodoCommand command)
        {
            if (id != command.Id) return BadRequest("Not valid Id");
            return Ok(await _mediator.Send(command));
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            await _mediator.Send(new DeleteTodoCommand(id));
            return NoContent();
        }
    }
}
