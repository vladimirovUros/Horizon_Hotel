using Application.DTO.Types;
using Application.UseCases;
using Application.UseCases.Commands.Types;
using Application.UseCases.Queries.Types;
using Implementation.UseCases;
using Implementation.UseCases.Commands.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public TypesController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<TypesController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchType search, [FromServices] IGetTypesQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));


        // GET api/<TypesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetTypeQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST api/<TypesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateTypeDTO dto, [FromServices] ICreateTypeCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<TypesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateTypeDTO dto, [FromServices] IUpdateTypeCommand command)
        {
            dto.Id = id;

            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(204);
        }

        // DELETE api/<TypesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICommand<int> command)
        {
            _useCaseHandler.HandleCommand(command, id);

            return StatusCode(204);
        }
    }
}
