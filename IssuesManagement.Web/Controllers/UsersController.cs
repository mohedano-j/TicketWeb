using Microsoft.AspNetCore.Mvc;
using IssuesManagement.Services;


namespace IssuesManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        // GET: api/<TicketsController>/
        [HttpGet()]
        public async Task<List<User>> GetAsync()
        {
            return await _userService.GetAsync();
        }

        // GET: api/<TicketsController>/search
        [HttpGet("search")]
        public async Task<List<User>> SearchAsync(string searchInput)
        {
            //Search by partial Id
            if(Int32.TryParse(searchInput, out int result))
            {
                return await _userService.SearchAsync(result);
            }

            var names = searchInput.Split(" ", 2);

            //Search by one name
            if(names.Length == 1)
            {
                return await _userService.SearchAsync(names[0]);
            }

            //Search by 2 names
            var firstName = names[0].Trim();
            var lastName = names[1].Trim();

            return await _userService.SearchAsync(firstName, lastName); 
        }        
    }
}
