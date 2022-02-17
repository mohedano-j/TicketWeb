using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> SearchAsync(int projectId);

        Task<Ticket> GetAsync(int id);

        Task<Ticket> AddAsync(Ticket ticket);

        Task<Ticket> EditAsync(Ticket ticket);

        Task<Ticket> DeleteAsync(Ticket ticket);
    }
}
