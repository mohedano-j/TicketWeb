using IssuesManagement.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IssuesManagement.Services.Tests.Unit.Data
{
	public class IssuesRepositoryTests
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
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected,
			new Issue { IssueId = 6, Title = "Issue 1", StatusOpened = true });

			db.SaveChanges();

			var repository = new IssuesRepository(db);
			//	Act
			var result = await repository.GetAsync(5);

			Assert.Equal(expected, result);

		}

		[Fact]
		public async void GetAsyncList()
		{
			//	Arrange
			var data = new List<Issue>() {
				new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true },
				new Issue() { IssueId = 6, Title = "Issue 2", StatusOpened = true },
				new Issue() { IssueId = 7, Title = "Issue 3", StatusOpened = true }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new IssuesRepository(db);

			//	Act
			var result = await repository.GetAsync();

			Assert.Equal(data.Count, result.Count);

		}

		[Fact]
		public async void AddAsync()
		{
			//	Arrange
			var data = new List<Issue>() {
				new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true },
				new Issue() { IssueId = 6, Title = "Issue 2", StatusOpened = true},
				new Issue() { IssueId = 7, Title = "Issue 3", StatusOpened = true}
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new IssuesRepository(db);


			//	Act
			var newRecord = new Issue() { Title = "Issue 4", StatusOpened = true };
			var result = await repository.AddAsync(newRecord);

            var numberOfRecords = await db.Issues.CountAsync();

			Assert.Equal(newRecord, result);
			Assert.Equal(4, numberOfRecords);

		}

		[Fact]
		public async void EditAsync()
		{
			//	Arrange
			var data = new List<Issue>() {
				new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true },
				new Issue() { IssueId = 6, Title = "Issue 2", StatusOpened = true },
				new Issue() { IssueId = 7, Title = "Issue 3", StatusOpened = true }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new IssuesRepository(db);


			//	Act
			var editRecord = new Issue() { IssueId = 5, Title = "Issue 1 Modified", StatusOpened = false } ;
			var result = await repository.EditAsync(editRecord);


			//Assert
			Assert.NotNull(result);
			Assert.False(result.StatusOpened);

			var numberOfRecords = await db.Issues.CountAsync();
			Assert.Equal(3, numberOfRecords);

		}

		public async void EditAsyncNotFound()
		{
			//	Arrange
			var data = new List<Issue>() {
				new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true },
				new Issue() { IssueId = 6, Title = "Issue 2", StatusOpened = true },
				new Issue() { IssueId = 7, Title = "Issue 3", StatusOpened = true }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new IssuesRepository(db);


			//	Act
			var editRecord = new Issue() { IssueId = 8, Title = "Issue 8 Modified", StatusOpened = false };
			var ex = await Assert.ThrowsAsync<ApplicationException>(async () => await repository.EditAsync(editRecord));

			//Assert
			Assert.Equal("Issue not found.", ex.Message);

		}
	}
}
