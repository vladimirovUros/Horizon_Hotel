using Application.DTO.Rooms;
using Application.UseCases;
using Application.UseCases.Commands.Rooms;
using Application.UseCases.Queries.Rooms;
using Domain;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public RoomsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<RoomsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchRoom search, [FromServices] IGetRoomsQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<RoomsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetRoomQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST api/<RoomsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDTO dto, [FromServices] ICreateRoomCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<RoomsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRoomDTO dto, [FromServices] IUpdateRoomCommand command)
        {
            dto.Id = id;

            _useCaseHandler.HandleCommand(command, dto);

            return StatusCode(204);
        }

        // DELETE api/<RoomsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICommand<int> command)
        {
            _useCaseHandler.HandleCommand(command, id);

            return StatusCode(204);
        }
    }
}
