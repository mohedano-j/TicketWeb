using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public interface IProjectRepository
    {
        Task<Project> GetAsync(int id);

        Task<Project> AddAsync(Project Project);

        Task<Project> EditAsync(Project Project);

        Task<Project> DeleteAsync(Project Project);
    }
}
