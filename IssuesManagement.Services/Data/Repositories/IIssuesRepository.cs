using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesManagement.Services.Data
{
    public interface IIssuesRepository
    {
        Task<List<Issue>> SearchAsync(int userId);

        Task<List<Issue>> GetAsync();

        Task<Issue> GetAsync(int id);

        Task<Issue> AddAsync(Issue ticket);

        Task<Issue> EditAsync(Issue ticket);
    }
}
