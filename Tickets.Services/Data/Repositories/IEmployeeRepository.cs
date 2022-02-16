using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(int id);

        Task<List<Employee>> GetAsync();

        Task<List<Employee>> SearchAsync(int partialId);

        Task<List<Employee>> SearchAsync(string name);

        Task<List<Employee>> SearchAsync(string firstName, string lastName);
    }
}
