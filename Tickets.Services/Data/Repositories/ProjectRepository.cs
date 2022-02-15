using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TicketSystemContext _db;
        public ProjectRepository(TicketSystemContext db)
        {
            _db = db;
        }

        public async Task<Project> AddAsync(Project project)
        {
            if(project.ProjectId != 0)
            {
               throw new ApplicationException("Assign a non-zero project Id.");
            }

            project.DateCreated = DateTime.Now;
            project.DateUpdated = DateTime.Now;

            await _db.Projects.AddAsync(project);

            await _db.SaveChangesAsync();

            return project;
        }

        public async Task<Project> DeleteAsync(Project project)
        {
            await CheckProjectExists(project);

            _db.Projects.Remove(project);

            await _db.SaveChangesAsync();

            return project;
        }

        public async Task<Project> EditAsync(Project project)
        {

            await CheckProjectExists(project);

            project.DateUpdated = DateTime.Now;

            _db.Projects.Update(project);

            await _db.SaveChangesAsync();

            return project;
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _db.Projects.FindAsync(id);
        }

        private async Task CheckProjectExists(Project project)
        {
            if (await _db.Projects.FindAsync(project.ProjectId) == null)
            {
                throw new ApplicationException("Project not found.");
            }
        }
    }
}
