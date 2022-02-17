using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesManagement.Services
{
    public interface IIssuesService
    {
        Task<List<Issue>> GetAsync();

        Task<Issue> GetAsync(int id);

        Task<Issue> AddAsync(Issue issue);

        Task<Issue> EditAsync(Issue issue);

        Task<List<Issue>> SearchAsync(int userId);
    }
}
