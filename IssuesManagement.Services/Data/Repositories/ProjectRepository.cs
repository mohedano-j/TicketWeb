﻿using Microsoft.EntityFrameworkCore;
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

            //Getting value to recalculate include properties.
            var result = await GetAsync(project.ProjectId);

            return result;
        }

        public async Task<Project> DeleteAsync(Project project)
        {
            var projectToDelete = await CheckProjectExists(project);

            _db.Projects.Remove(projectToDelete);

            await _db.SaveChangesAsync();

            return projectToDelete;
        }

        public async Task<Project> EditAsync(Project project)
        {

            var projectToEdit = await CheckProjectExists(project);

            projectToEdit.ProjectName = project.ProjectName;
            projectToEdit.Description = project.Description;
            projectToEdit.StatusCode = project.StatusCode;
            projectToEdit.DateUpdated = DateTime.Now;

            await _db.SaveChangesAsync();

            //Getting value to recalculate include properties.
            var result = await GetAsync(projectToEdit.ProjectId);

            return result;
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _db.Projects.Include(p => p.Status).FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<List<Project>> GetAsync()
        {
            return await _db.Projects.Include(p=>p.Status).ToListAsync();
        }

        private async Task<Project> CheckProjectExists(Project project)
        {
            var result = await GetAsync(project.ProjectId);
            if (result == null)
            {
                throw new ApplicationException("Project not found.");
            }

            return result;
        }
    }
}