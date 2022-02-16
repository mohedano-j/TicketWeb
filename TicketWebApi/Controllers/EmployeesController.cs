using Microsoft.AspNetCore.Mvc;
using Tickets.Services;


namespace Tickets.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;
        public EmployeesController(IEmployeesService employeesService)
        {
            _employeeService = employeesService;
        }

        // GET: api/<TicketsController>/
        [HttpGet()]
        public async Task<List<Employee>> GetAsync()
        {
            return await _employeeService.GetAsync();
        }

        // GET: api/<TicketsController>/search
        [HttpGet("search")]
        public async Task<List<Employee>> SearchAsync(string searchInput)
        {
            //Search by partial Id
            if(Int32.TryParse(searchInput, out int result))
            {
                return await _employeeService.SearchAsync(result);
            }

            var names = searchInput.Split(" ", 2);

            //Search by one name
            if(names.Length == 1)
            {
                return await _employeeService.SearchAsync(names[0]);
            }

            //Search by 2 names
            var firstName = names[0].Trim();
            var lastName = names[1].Trim();

            return await _employeeService.SearchAsync(firstName, lastName); 
        }        
    }
}
