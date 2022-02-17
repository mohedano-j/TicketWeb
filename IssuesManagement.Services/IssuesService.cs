using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssuesManagement.Services.Data;

namespace IssuesManagement.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IIssuesRepository _repo;

        public IssuesService(IIssuesRepository repo)
        {
            _repo = repo;
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            return await _repo.AddAsync(issue);
        }

        public async Task<Issue> EditAsync(Issue issue)
        {
            return await _repo.EditAsync(issue);
        }

        public async Task<Issue> GetAsync(int id)
        {
            return await _repo.GetAsync(id);
        }

        public async Task<List<Issue>> GetAsync()
        {
            return await _repo.GetAsync();
        }

        public async Task<List<Issue>> SearchAsync(int userId)
        {
            return await _repo.SearchAsync(userId);
        }
    }
}
