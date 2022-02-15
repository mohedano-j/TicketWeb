using Microsoft.AspNetCore.Mvc;
using Tickets.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tickets.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/<TicketsController>/id
        [HttpGet]
        public async Task<List<Status>> GetAsync()
        {
            return await _statusService.GetAsync();
        }        
    }
}
