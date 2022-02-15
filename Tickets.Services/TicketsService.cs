using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;

namespace Tickets.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IProjectsService _projectsService;

        public TicketsService(ITicketRepository ticketRepository, IProjectsService projectsService)
        {
            _ticketRepository = ticketRepository;
            _projectsService = projectsService;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await CheckProjectExists(ticket);

            return await _ticketRepository.AddAsync(ticket);
        }

        public Task<Ticket> DeleteAsync(Ticket ticket)
        {
           return _ticketRepository.DeleteAsync(ticket);
        }

        public async Task<Ticket> EditAsync(Ticket ticket)
        {
            await CheckProjectExists(ticket);
            return await _ticketRepository.EditAsync(ticket);
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _ticketRepository.GetAsync(id);
        }

        private async Task CheckProjectExists(Ticket ticket)
        {
            if (await _projectsService.GetAsync(ticket.ProjectId) == null)
            {
                throw new ApplicationException("Project was not found and ticket could not be created.");
            }
        }
    }
}
