﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TicketSystemContext _db;
        public EmployeeRepository(TicketSystemContext db)
        {
            _db = db;
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> SearchAsync(int partialId)
        {
            return await _db.Employees.Where(e=>e.EmployeeId.ToString().Contains(partialId.ToString())).ToListAsync();
        }

        public async Task<List<Employee>> SearchAsync(string name)
        {
            return await _db.Employees
              .Where(e => (e.FirstName.Contains(name))
               || e.LastName.Contains(name))
              .ToListAsync();
        }

        public async Task<List<Employee>> SearchAsync(string firstName, string lastName)
        {
            return await _db.Employees
                .Where(e => (e.FirstName.Contains(firstName)) 
                 && e.LastName.Contains(lastName))
                .ToListAsync();
        }
    }
}