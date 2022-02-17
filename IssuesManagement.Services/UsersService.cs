using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssuesManagement.Services.Data;

namespace IssuesManagement.Services
{
    public class UsersService :IUsersService
    {
        public readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userRepository.GetAsync();
        }

        public async Task<List<User>> SearchAsync(int partialId)
        {
            return await _userRepository.SearchAsync(partialId);
        }

        public async Task<List<User>> SearchAsync(string name)
        {
            return await _userRepository.SearchAsync(name);
        }

        public async Task<List<User>> SearchAsync(string firstName, string lastName)
        {
            return await _userRepository.SearchAsync(firstName, lastName);
        }
    }
}
