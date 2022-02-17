using IssuesManagement.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IssuesManagement.Services.Tests.Unit.Data
{
	public class UsersRepositoryTests
	{

		public IssuesDbContext GetInMemoryDbContext()
		{
			IssuesDbContext db = IssuesDbContextInMemoryBuilder.BuildInMemoryDbContext();

			return db;
		}


		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new User() { UserId = 5, UserName="AAA", FirstName = "Joe", LastName = "Doe" };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected,
			new User { UserId = 50, UserName = "AAA", FirstName = "Mary", LastName = "Doe" });

			db.SaveChanges();

			var repository = new UsersRepository(db);


			//	Act
			var result = await repository.GetAsync(5);

			// Assert
			Assert.Equal(expected, result);

		}

		[Fact]
		public async void GetAsyncList()
		{
			//	Arrange
			var data = new List<User>() {
				new User{ UserId = 5, UserName="AAA", FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, UserName="AAA", FirstName = "Mary", LastName="Smith"},
				new User { UserId = 60, UserName="AAA", FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new UsersRepository(db);


			//	Act
			var result = await repository.GetAsync();

			//Assert
			Assert.Equal(3, result.Count);
			Assert.Equal("Mary", result[2].FirstName);
		}

		[Fact]
		public async void SearchAsyncByPartialId()
		{
			//	Arrange
			var data = new List<User>() {
				new User{ UserId = 5, UserName="AAA", FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, UserName="AAA", FirstName = "Mary", LastName="Smith"},
				new User { UserId = 60, UserName="AAA", FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new UsersRepository(db);


			//	Act
			var result = await repository.SearchAsync(5);

			Assert.Equal(2, result.Count);

		}

		[Fact]
		public async void SearchAsyncByName()
		{
			//	Arrange
			var data = new List<User>() {
				new User{ UserId = 5, UserName="AAA", FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, UserName="AAA", FirstName = "Mary", LastName="Smith"},
				new User { UserId = 60, UserName="AAA", FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new UsersRepository(db);


			//	Act
			var result = await repository.SearchAsync("Smith");

			Assert.Equal(2, result.Count);

		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastName()
		{
			//	Arrange
			var data = new List<User>() {
				new User{ UserId = 5, UserName="AAA", FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, UserName="AAA", FirstName = "Mary", LastName="Smith"},
				new User { UserId = 60, UserName="AAA", FirstName = "Luis", LastName="Smith"}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new UsersRepository(db);

			//	Act
			var result = await repository.SearchAsync("Mary","Smith");
			Assert.Single(result);

		}
	}
}
