using Microsoft.AspNetCore.Mvc;
using IssuesManagement.Services;

namespace IssuesManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssuesService _issueService;
        public IssuesController(IIssuesService issuesService)
        {
            _issueService = issuesService;
        }

        [HttpGet]
        public async Task<List<Issue>> GetAsync()
        {
            return await _issueService.GetAsync();
        }

      

        // GET: api/<IssuesController>/id
        [HttpGet("{id}")]
        public async Task<Issue> GetAsync(int id)
        {
            return await _issueService.GetAsync(id);
        }

        // GET: api/<IssuesController>/search/userid
        [HttpGet("search/{userId}")]
        public async Task<List<Issue>> SearchAsync(int userId)
        {
            return await _issueService.SearchAsync(userId);
        }

        // POST api/<IssuesController>
        [HttpPost]
        public async Task<Issue> PostAsync(Issue issue)
        {
            return await _issueService.AddAsync(issue);
        }

        // PUT api/<IssuesController>/5
        [HttpPut]
        public async Task<Issue> PutAsync(Issue issue)
        {
            return await _issueService.EditAsync(issue);
        }
    }
}
