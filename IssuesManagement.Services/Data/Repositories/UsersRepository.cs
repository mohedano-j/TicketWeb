using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesManagement.Services.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly  IssuesDbContext _db;
        public UsersRepository(IssuesDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAsync()
        {
            return await _db.Users
                .OrderBy(e=>e.LastName).ThenBy(e=>e.FirstName).ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<List<User>> SearchAsync(int partialId)
        {
            return await _db.Users.Where(e=>e.UserId.ToString().Contains(partialId.ToString()))
                .OrderBy(e => e.LastName).ThenBy(e => e.LastName).ToListAsync();
        }

        public async Task<List<User>> SearchAsync(string name)
        {
            return await _db.Users
              .Where(e => (e.FirstName.Contains(name))
               || e.LastName.Contains(name))
              .OrderBy(e => e.LastName).ThenBy(e => e.LastName)
              .ToListAsync();
        }

        public async Task<List<User>> SearchAsync(string firstName, string lastName)
        {
            return await _db.Users
                .Where(e => (e.FirstName.Contains(firstName)) 
                 && e.LastName.Contains(lastName))
                .OrderBy(e => e.LastName).ThenBy(e => e.LastName)
                .ToListAsync();
        }
    }
}
