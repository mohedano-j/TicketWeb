using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketSystemContext _db;
        public TicketRepository(TicketSystemContext db)
        {
            _db = db;
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _db.Tickets.Include(t => t.Status).Include(t=>t.Employee).FirstOrDefaultAsync(t=>t.TicketId == id);
        }

        public async Task<List<Ticket>> SearchAsync(int projectId)
        {
            return await _db.Tickets.Where(t => t.ProjectId == projectId).Include(t => t.Status).Include(t => t.Employee).ToListAsync();
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            if(ticket.TicketId != 0)
            {
               throw new ApplicationException("Assign a non-zero ticket Id.");
            }

            ticket.DateCreated = DateTime.Now;
            ticket.DateUpdated = DateTime.Now;

            await _db.Tickets.AddAsync(ticket);

            await _db.SaveChangesAsync();

            //Getting value to recalculate include properties.
            var result = await GetAsync(ticket.TicketId);

            return result;
        }

        public async Task<Ticket> DeleteAsync(Ticket ticket)
        {
            var ticketToDelete = await CheckTicketExists(ticket);

            _db.Tickets.Remove(ticket);

            await _db.SaveChangesAsync();

            return ticket;
        }

    

        public async Task<Ticket> EditAsync(Ticket ticket)
        {

            var ticketToUpdate = await CheckTicketExists(ticket);

            ticketToUpdate.EmployeeId = ticket.EmployeeId;
            ticketToUpdate.Title = ticket.Title;
            ticketToUpdate.ProjectId = ticket.ProjectId;
            ticketToUpdate.Description = ticket.Description;
            ticketToUpdate.TimeSpend = ticket.TimeSpend;
            ticketToUpdate.TimeRemaining = ticket.TimeRemaining;
            ticketToUpdate.DateUpdated = DateTime.Now;

            await _db.SaveChangesAsync();

            //Getting value to recalculate include properties.
            var result = await GetAsync(ticketToUpdate.TicketId);

            return result;
        }


        private async Task<Ticket> CheckTicketExists(Ticket ticket)
        {
            var result = await GetAsync(ticket.TicketId);

            if (result == null)
            {
                throw new ApplicationException("Ticket not found.");
            }

            return result;
        }
    }
}
