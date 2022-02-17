using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesManagement.Services.Data
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly IssuesDbContext _db;
        public IssuesRepository(IssuesDbContext db)
        {
            _db = db;
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            if(issue.IssueId != 0)
            {
               throw new ApplicationException("Assign a non-zero issue Id.");
            }

            await _db.Issues.AddAsync(issue);

            await _db.SaveChangesAsync();

            //Getting value to recalculate include properties.
            var result = await GetAsync(issue.IssueId);

            return issue;
        }


        public async Task<Issue> EditAsync(Issue issue)
        {

            var issueToEdit = await CheckIssueExists(issue);

            issueToEdit.Title = issue.Title;
            issueToEdit.Description = issue.Description;
            issueToEdit.StatusOpened = issue.StatusOpened;
            issueToEdit.AssignedTo = issue.AssignedTo;

            await _db.SaveChangesAsync();

            //Getting value to recalculate include properties.
            var result = await GetAsync(issue.IssueId);

            return issueToEdit;
        }

        public async Task<Issue> GetAsync(int id)
        {
            return await _db.Issues.Include(i=>i.AssignedToNavigation).FirstOrDefaultAsync(i=>i.IssueId == id);
        }

        public async Task<List<Issue>> GetAsync()
        {
            return await _db.Issues.Include(i => i.AssignedToNavigation).ToListAsync();
        }

        public async Task<List<Issue>> SearchAsync(int userId)
        {
            return await _db.Issues.Include(i => i.AssignedToNavigation).Where(i=>i.AssignedTo == userId).ToListAsync();
        }

        private async Task<Issue> CheckIssueExists(Issue issue)
        {
            var result = await GetAsync(issue.IssueId);
            if (result == null)
            {
                throw new ApplicationException("Issue not found.");
            }

            return result;
        }
    }
}
