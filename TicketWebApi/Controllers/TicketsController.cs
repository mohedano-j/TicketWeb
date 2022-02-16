using Microsoft.AspNetCore.Mvc;
using Tickets.Services;


namespace Tickets.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketService;
        public TicketsController(ITicketsService ticketsService)
        {
            _ticketService = ticketsService;
        }

        // GET: api/<TicketsController>/id
        [HttpGet("{id}")]
        public async Task<Ticket> GetAsync(int id)
        {
            return await _ticketService.GetAsync(id);
        }

        // GET: api/<TicketsController>/search/projectId
        [HttpGet("search/{projectId}")]
        public async Task<List<Ticket>> SearchAsync(int projectId)
        {
            return await _ticketService.SearchAsync(projectId);
        }


        // POST api/<TicketsController>
        [HttpPost]
        public async Task<Ticket> PostAsync(Ticket ticket)
        {
            return await _ticketService.AddAsync(ticket);
        }

        // PUT api/<TicketsController>/5
        [HttpPut]
        public async Task<Ticket> PutAsync(Ticket ticket)
        {
            return await _ticketService.EditAsync(ticket);
        }

        // DELETE api/<TicketsController>
        [HttpDelete]
        public async Task<Ticket> DeleteAsync(Ticket ticket)
        {
            return await _ticketService.DeleteAsync(ticket);
        }
    }
}
