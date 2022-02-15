using Microsoft.AspNetCore.Mvc;
using Tickets.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        // POST api/<TicketsController>
        [HttpPost]
        public async Task<Ticket> Post(Ticket ticket)
        {
            return await _ticketService.AddAsync(ticket);
        }

        // PUT api/<TicketsController>/5
        [HttpPut]
        public async Task<Ticket> Put(Ticket ticket)
        {
            return await _ticketService.EditAsync(ticket);
        }

        // DELETE api/<TicketsController>
        [HttpDelete]
        public async Task<Ticket> Delete(Ticket ticket)
        {
            return await _ticketService.DeleteAsync(ticket);
        }
    }
}
