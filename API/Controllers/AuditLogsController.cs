using Application.DTO.AuditLogs;
using Application.UseCases.Queries.AuditLogs;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public AuditLogsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<AuditLogsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchAuditLog search, [FromServices] IGetAuditLogsQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));
    }
}
