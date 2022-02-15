using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services
{
    public interface IEmployeesService
    {
        Task<Employee> GetAsync(int id);

        Task<List<Employee>> SearchAsync(int partialId);

        Task<List<Employee>> SearchAsync(string name);

        Task<List<Employee>> SearchAsync(string firstName, string lastName);
    }
}
