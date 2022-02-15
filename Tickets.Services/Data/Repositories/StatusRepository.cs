using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public class StatusRepository : IStatusRepository
    {
        private readonly TicketSystemContext _db;
        public StatusRepository(TicketSystemContext db)
        {
            _db = db;
        }

    
        public async Task<List<Status>> GetAsync()
        {
            return await _db.Status.ToListAsync();
        }

    }
}
