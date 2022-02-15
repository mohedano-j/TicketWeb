using Microsoft.AspNetCore.Mvc;
using Tickets.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tickets.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectService;
        public ProjectsController(IProjectsService projectsService)
        {
            _projectService = projectsService;
        }

        [HttpGet]
        public async Task<List<Project>> GetAsync()
        {
            return await _projectService.GetAsync();
        }

        // GET: api/<ProjectsController>/id
        [HttpGet("{id}")]
        public async Task<Project> GetAsync(int id)
        {
            return await _projectService.GetAsync(id);
        }


        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<Project> Post(Project project)
        {
            return await _projectService.AddAsync(project);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut]
        public async Task<Project> Put(Project project)
        {
            return await _projectService.EditAsync(project);
        }

        // DELETE api/<ProjectsController>
        [HttpDelete]
        public async Task<Project> Delete(Project project)
        {
            return await _projectService.DeleteAsync(project);
        }
    }
}
