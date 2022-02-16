using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;
using Xunit;

namespace Tickets.Services.Tests.Unit.Data
{
	public class EmployeeRepositoryTests
	{

		public TicketSystemContext GetInMemoryDbContext()
        {
            TicketSystemContext db = TycketSytemContextInMemoryBuilder.BuildInMemoryDbContext();
            return db;
        }



        [Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Employee() { EmployeeId = 5, FirstName = "Joe", LastName = "Doe" };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected,
			new Employee { EmployeeId = 50, FirstName = "Mary", LastName = "Doe" });

			db.SaveChanges();

			var repository = new EmployeeRepository(db);


			//	Act
			var result = await repository.GetAsync(5);

			Assert.Equal(expected, result);

		}

		[Fact]
		public async void SearchAsyncByPartialId()
		{
			//	Arrange
			var data = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"},
				new Employee { EmployeeId = 60, FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new EmployeeRepository(db);


			//	Act
			var result = await repository.SearchAsync(5);

			Assert.Equal(2, result.Count);

		}

		[Fact]
		public async void SearchAsyncByName()
		{
			//	Arrange
			var data = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"},
				new Employee { EmployeeId = 60, FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new EmployeeRepository(db);


			//	Act
			var result = await repository.SearchAsync("Smith");

			Assert.Equal(2, result.Count);

		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastName()
		{
			//	Arrange
			var data = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"},
				new Employee { EmployeeId = 60, FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new EmployeeRepository(db);


			//	Act
			var result = await repository.SearchAsync("Mary","Smith");


			Assert.Single(result);

		}
	}
}
