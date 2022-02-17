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

            return issue;
        }

        public async Task<Issue> DeleteAsync(Issue issue)
        {
            var issueToDelete = await CheckIssueExists(issue);

            _db.Issues.Remove(issueToDelete);

            await _db.SaveChangesAsync();

            return issueToDelete;
        }

        public async Task<Issue> EditAsync(Issue issue)
        {

            var issueToEdit = await CheckIssueExists(issue);

            issueToEdit.Title = issue.Title;
            issueToEdit.Description = issue.Description;
            issueToEdit.StatusOpened = issue.StatusOpened;

            await _db.SaveChangesAsync();

            return issueToEdit;
        }

        public async Task<Issue> GetAsync(int id)
        {
            return await _db.Issues.FindAsync(id);
        }

        public async Task<List<Issue>> GetAsync()
        {
            return await _db.Issues.ToListAsync();
        }

        public Task<List<Issue>> SearchAsync(int employeeId)
        {
            throw new NotImplementedException();
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
