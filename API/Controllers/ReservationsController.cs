using Application.DTO.Reservations;
using Application.Exceptions;
using Application.UseCases;
using Application.UseCases.Commands.Reservations;
using Application.UseCases.Queries.Reservations;
using DataAccess;
using FluentValidation;
using Implementation.UseCases;
using Implementation.Validators.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        private readonly HotelHorizonContext _context;
        private readonly CheckReservationDtoValidator _validator;

        public ReservationsController(UseCaseHandler useCaseHandler, HotelHorizonContext context, CheckReservationDtoValidator validator)
        {
            _useCaseHandler = useCaseHandler;
            _context = context;
            _validator = validator;
        }

        // GET: api/<ReservationsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchReservation search, [FromServices] IGetReservationsQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpGet("myreservations")]
        public IActionResult GetMyReservations([FromQuery] SearchReservation search, [FromServices] IGetReservationsQuery query)
        {
            return Ok(_useCaseHandler.HandleQuery(query, search));
        }

        // POST api/<ReservationsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationDTO dto, [FromServices] ICreateReservationCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ReservationsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateReservationDTO dto, [FromServices] IUpdateReservationCommand command)
        {
            dto.ReservationId = id;
            _useCaseHandler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<ReservationsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICommand<int> command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }
        [Authorize]
        [HttpGet ("check")]
        public IActionResult Check([FromQuery] CheckReservationDTO dto)
        {
            _validator.ValidateAndThrow(dto);

            bool roomIsNotAvailable = _context.OccupiedRooms.Any(o => o.RoomId == dto.RoomId && (o.Date == dto.CheckIn || o.Date == dto.CheckOut));
            if (roomIsNotAvailable)
            {
                throw new ConflictException("Room is not available for selected dates.");
            }
            return Ok();
        }
    }
}
