using Microsoft.AspNetCore.Mvc;
using Tickets.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tickets.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        // GET: api/<TicketsController>/search
        [HttpGet("search")]
        public async Task<List<Employee>> SearchAsync(string searchInput)
        {
            //Search by partial Id
            if(Int32.TryParse(searchInput, out int result))
            {
                return await _employeesService.SearchAsync(result);
            }

            var names = searchInput.Split(" ", 2);

            //Search by one name
            if(names.Length == 1)
            {
                return await _employeesService.SearchAsync(names[0]);
            }

            //Search by 2 names
            var firstName = names[0].Trim();
            var lastName = names[1].Trim();

            return await _employeesService.SearchAsync(firstName, lastName); 
        }        
    }
}
