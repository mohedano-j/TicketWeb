using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;

namespace Tickets.Services
{
    public class StatusService :IStatusService
    {
        public readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<Status>> GetAsync()
        {
            return await _statusRepository.GetAsync();
        }
    }
}
