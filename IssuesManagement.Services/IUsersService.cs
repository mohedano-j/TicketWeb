using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesManagement.Services
{
    public interface IUsersService
    {
        Task<User> GetAsync(int id);

        Task<List<User>> GetAsync();

        Task<List<User>> SearchAsync(int partialId);

        Task<List<User>> SearchAsync(string name);

        Task<List<User>> SearchAsync(string firstName, string lastName);
    }
}
