using Application.DTO.Beds;
using Application.UseCases;
using Application.UseCases.Commands.Beds;
using Application.UseCases.Queries.Beds;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedsController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public BedsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<BedsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchBed search, [FromServices] IGetBedsQuery query) => 
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<BedsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetBedQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST api/<BedsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBedDTO dto, [FromServices] ICreateBedCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<BedsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBedDTO dto, [FromServices] IUpdateBedCommand command)
        {
            dto.Id = id;

            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(204);
        }

        // DELETE api/<BedsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICommand<int> command)
        {
            _useCaseHandler.HandleCommand(command, id);

            return StatusCode(204);
        }
    }
}
