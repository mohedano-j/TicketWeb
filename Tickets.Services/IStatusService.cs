using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetAsync();
    }
}
