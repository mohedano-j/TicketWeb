using Microsoft.AspNetCore.Mvc;
using Tickets.Services;

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
        public async Task<Project> PostAsync(Project project)
        {
            return await _projectService.AddAsync(project);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut]
        public async Task<Project> PutAsync(Project project)
        {
            return await _projectService.EditAsync(project);
        }

        // DELETE api/<ProjectsController>
        [HttpDelete]
        public async Task<Project> DeleteAsync(Project project)
        {
            return await _projectService.DeleteAsync(project);
        }
    }
}
