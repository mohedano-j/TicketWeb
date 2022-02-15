using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;

namespace Tickets.Services
{
    public class EmployeesService :IEmployeesService
    {
        public readonly IEmployeeRepository _employeeRepository;

        public EmployeesService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _employeeRepository.GetAsync(id);
        }

        public async Task<List<Employee>> SearchAsync(int partialId)
        {
            return await _employeeRepository.SearchAsync(partialId);
        }

        public async Task<List<Employee>> SearchAsync(string name)
        {
            return await _employeeRepository.SearchAsync(name);
        }

        public async Task<List<Employee>> SearchAsync(string firstName, string lastName)
        {
            return await _employeeRepository.SearchAsync(firstName, lastName);
        }
    }
}
