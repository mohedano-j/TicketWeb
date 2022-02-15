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

            return ticket;
        }

        public async Task<Ticket> DeleteAsync(Ticket ticket)
        {
            await CheckTicketExists(ticket);

            _db.Tickets.Remove(ticket);

            await _db.SaveChangesAsync();

            return ticket;
        }

    

        public async Task<Ticket> EditAsync(Ticket ticket)
        {

            await CheckTicketExists(ticket);

            ticket.DateUpdated = DateTime.Now;

            _db.Tickets.Update(ticket);

            await _db.SaveChangesAsync();

            return ticket;
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _db.Tickets.FindAsync(id);
        }

        private async Task CheckTicketExists(Ticket ticket)
        {
            if (await _db.Tickets.FindAsync(ticket.TicketId) == null)
            {
                throw new ApplicationException("Ticket not found.");
            }
        }
    }
}
