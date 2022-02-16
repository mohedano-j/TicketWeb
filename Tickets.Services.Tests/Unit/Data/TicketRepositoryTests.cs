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
	public class TicketRepositoryTests
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
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", Description = "aaa", StatusCode = "T", ProjectId = 1 };

			var db = GetInMemoryDbContext();

			db.AddRange(
			expected,
			new Ticket { TicketId = 6, Title = "Ticket 2", StatusCode = "T", ProjectId = 1 });

			db.SaveChanges();

			var repository = new TicketRepository(db);


			//	Act
			var result = await repository.GetAsync(5);

			Assert.Equal(expected, result);

		}

		[Fact]
		public async void SearchAsync()
		{
			//	Arrange
			var data = new List<Ticket>() {
				new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 6, Title = "Ticket 2", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 7, Title = "Ticket 3", StatusCode = "T", ProjectId = 2 }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new TicketRepository(db);

			//	Act
			var result = await repository.SearchAsync(1);

			Assert.Equal(2, result.Count);

		}

		[Fact]
		public async void AddAsync()
		{
			//	Arrange
			var data = new List<Ticket>() {
				new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 6, Title = "Ticket 2", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 7, Title = "Ticket 3", StatusCode = "T", ProjectId = 2 }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new TicketRepository(db);


			//	Act
			var newRecord = new Ticket() { Title = "Ticket 4", StatusCode = "T", ProjectId = 1 };
			var result = await repository.AddAsync(newRecord);

            var numberOfRecords = await db.Tickets.CountAsync();

			Assert.Equal(newRecord, result);
			Assert.Equal(4, numberOfRecords);

		}

		[Fact]
		public async void EditAsync()
		{
			//	Arrange
			var data = new List<Ticket>() {
				new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 6, Title = "Ticket 2", StatusCode = "T", ProjectId = 1 },
				new Ticket() { TicketId = 7, Title = "Ticket 3", StatusCode = "T", ProjectId = 2 }
			};

			var db = GetInMemoryDbContext();

			db.AddRange(data);

			db.SaveChanges();

			var repository = new TicketRepository(db);


			//	Act
			var editRecord = new Ticket() { TicketId = 5, Title = "Ticket 1 Modified", StatusCode = "T", ProjectId = 1 } ;
			var result = await repository.EditAsync(editRecord);


			//Assert
			var record = await db.Tickets.FindAsync(5);
			Assert.NotNull(record);
			Assert.Equal("Ticket 1 Modified", record.Title);

			var numberOfRecords = await db.Tickets.CountAsync();
			Assert.Equal(3, numberOfRecords);

		}
	}
}
