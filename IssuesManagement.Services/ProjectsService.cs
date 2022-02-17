using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;

namespace Tickets.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> AddAsync(Project project)
        {
            return await _projectRepository.AddAsync(project);
        }

        public Task<Project> DeleteAsync(Project project)
        {
           return _projectRepository.DeleteAsync(project);
        }

        public async Task<Project> EditAsync(Project project)
        {
            return await _projectRepository.EditAsync(project);
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _projectRepository.GetAsync(id);
        }

        public async Task<List<Project>> GetAsync()
        {
            return await _projectRepository.GetAsync();
        }
    }
}
