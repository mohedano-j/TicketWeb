using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services
{
    public interface IProjectsService
    {
        Task<Project> GetAsync(int id);

        Task<Project> AddAsync(Project project);

        Task<Project> EditAsync(Project project);

        Task<Project> DeleteAsync(Project project);
    }
}
