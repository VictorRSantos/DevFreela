using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjetsController(IMediator mediator)
        {

            _mediator = mediator;
        }

        // api/projects?query= net core
        [HttpGet]
        [Authorize(Roles ="client, freelancer")]
        public async Task<IActionResult> Get(string query) => Ok(await _mediator.Send(new GetAllProjectsQuery(query)));


        // api/projects/2
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));

            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/projects/3
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200) return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/comments
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_projectService.CreateComment(command);

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }



        // apí/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }


        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {

            command = new FinishProjectCommand(id, command.CreditCardNumber,command.Cvv, command.ExpiresAt, command.FullName);

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("O Pagameto não pode ser processado");
            }

            return NoContent();
        }

    }
}
