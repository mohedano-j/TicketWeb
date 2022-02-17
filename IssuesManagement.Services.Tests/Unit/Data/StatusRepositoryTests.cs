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
	public class StatusRepositoryTests
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
			var expected = new List<Status>() { new Status { StatusCode = "T", StatusDesc= "To Do" },
				new Status { StatusCode = "D", StatusDesc= "Done" } };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected);

			db.SaveChanges();

			var repository = new StatusRepository(db);

			//	Act
			var result = await repository.GetAsync();

			Assert.Equal(expected, result);

		}

		[Fact]
		public async void GetAsyncNull()
		{
			//Arrange
			var db = GetInMemoryDbContext();

			var repository = new StatusRepository(db);


			//	Act
			var result = await repository.GetAsync();

			// Assert
			Assert.Empty(result);
		}
	}
}
