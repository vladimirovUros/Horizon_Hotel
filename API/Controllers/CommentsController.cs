using Application.DTO.Comments;
using Application.UseCases.Commands.Comments;
using Application.UseCases.Queries.Comments;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public CommentsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<CommentsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchComment search, [FromServices] IGetCommentsQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<CommentsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCommentQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST api/<CommentsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDTO dto, [FromServices] ICreateCommentCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<CommentsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCommentDTO dto, [FromServices] IUpdateCommentCommand command)
        {
            dto.Id = id;

            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(204);
        }

        // DELETE api/<CommentsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
