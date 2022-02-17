using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAsync();
    }
}
