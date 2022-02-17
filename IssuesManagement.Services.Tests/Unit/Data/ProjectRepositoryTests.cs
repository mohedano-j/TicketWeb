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
	public class ProjectRepositoryTests
	{

		public TicketSystemContext GetInMemoryDbContext()
        {
            TicketSystemContext db = TycketSytemContextInMemoryBuilder.BuildInMemoryDbContext();

			// Add Statused. This is needed to get navigation propertie
			var statuses = new List<Status>() { new Status { StatusCode = "T", StatusDesc= "To Do" },
				new Status { StatusCode = "D", StatusDesc= "Done" } };

			db.AddRange(statuses);

			return db;
        }



        [Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected,
			new Project { ProjectId = 6, ProjectName = "Project 2", StatusCode = "T" });

			db.SaveChanges();

			var repository = new ProjectRepository(db);
			//	Act
			var result = await repository.GetAsync(5);

			Assert.Equal(expected, result);

		}

		[Fact]
		public async void GetAsyncList()
		{
			//	Arrange
			var data = new List<Project>() {
				new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" },
				new Project() { ProjectId = 6, ProjectName = "Project 2", StatusCode = "T" },
				new Project() { ProjectId = 7, ProjectName = "Project 3", StatusCode = "T" }
			};

			var statuses = new List<Status>() { new Status { StatusCode = "T", StatusDesc= "To Do" },
				new Status { StatusCode = "D", StatusDesc= "Done" } };

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new ProjectRepository(db);

			//	Act
			var result = await repository.GetAsync();

			Assert.Equal(data.Count, result.Count);

		}

		[Fact]
		public async void AddAsync()
		{
			//	Arrange
			var data = new List<Project>() {
				new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" },
				new Project() { ProjectId = 6, ProjectName = "Project 2", StatusCode = "T" },
				new Project() { ProjectId = 7, ProjectName = "Project 3", StatusCode = "T" }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new ProjectRepository(db);


			//	Act
			var newRecord = new Project() { ProjectName = "Project 4", StatusCode = "T" };
			var result = await repository.AddAsync(newRecord);

            var numberOfRecords = await db.Projects.CountAsync();

			Assert.Equal(newRecord, result);
			Assert.Equal(4, numberOfRecords);

		}

		[Fact]
		public async void EditAsync()
		{
			//	Arrange
			var data = new List<Project>() {
				new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" },
				new Project() { ProjectId = 6, ProjectName = "Project 2", StatusCode = "T" },
				new Project() { ProjectId = 7, ProjectName = "Project 3", StatusCode = "T" }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new ProjectRepository(db);


			//	Act
			var editRecord = new Project() { ProjectId = 5, ProjectName = "Project 1 Modified", StatusCode = "T" } ;
			var result = await repository.EditAsync(editRecord);


			//Assert
			var record = await db.Projects.FindAsync(5);
			Assert.Equal("Project 1 Modified", record.ProjectName);

			var numberOfRecords = await db.Projects.CountAsync();
			Assert.Equal(3, numberOfRecords);

		}
	}
}
