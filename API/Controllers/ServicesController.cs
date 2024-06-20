using Application.DTO.Services;
using Application.UseCases;
using Application.UseCases.Commands.Services;
using Application.UseCases.Queries.Services;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public ServicesController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<ServiceController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchService search, [FromServices] IGetServicesQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetServiceQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST api/<ServiceController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateServiceDTO dto, [FromServices] ICreateServiceCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ServiceController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateServiceDTO dto, [FromServices] IUpdateServiceCommand command)
        {
            dto.Id = id;

            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(204);
        }

        // DELETE api/<ServiceController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICommand<int> command)
        {
            _useCaseHandler.HandleCommand(command, id);

            return StatusCode(204);
        }
    }
}
