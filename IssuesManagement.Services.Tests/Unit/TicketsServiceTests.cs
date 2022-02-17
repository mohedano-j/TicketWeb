using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Services.Data;
using Xunit;

namespace Tickets.Services.Tests
{
	public class TicketsServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };
		

			var ticketRepoMock = new Mock<ITicketRepository>();
			ticketRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var project = new Project() { ProjectId = 1, ProjectName = "Project 1", StatusCode = "T" };
			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(project);

			var service = new TicketsService(ticketRepoMock.Object, projectServiceMock.Object);

			//	Act
			var result = await service.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T" };

			var ticketRepoMock = new Mock<ITicketRepository>();
			ticketRepoMock.Setup(i => i.AddAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);

			var project = new Project() { ProjectId = 1, ProjectName = "Project 1", StatusCode = "T" };
			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(project);
			var service = new TicketsService(ticketRepoMock.Object, projectServiceMock.Object);

			//	Act
			var result = await service.AddAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async Task AddAsyncNoPoject()
		{
			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T" };

			var ticketRepoMock = new Mock<ITicketRepository>();
			ticketRepoMock.Setup(i => i.AddAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);

			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value:null);
			var service = new TicketsService(ticketRepoMock.Object, projectServiceMock.Object);

			//	Act
			var ex = await Assert.ThrowsAsync<ApplicationException>(() => service.AddAsync(expected));

			Assert.Equal("Project was not found and ticket could not be created.", ex.Message);
		}



		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var ticketRepoMock = new Mock<ITicketRepository>();
			ticketRepoMock.Setup(i => i.EditAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);

			var project = new Project() { ProjectId = 1, ProjectName = "Project 1", StatusCode = "T" };
			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(project);
			var service = new TicketsService(ticketRepoMock.Object, projectServiceMock.Object);

			//	Act
			var result = await service.EditAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void DeleteAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var ticketRepoMock = new Mock<ITicketRepository>();
			ticketRepoMock.Setup(i => i.DeleteAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);


			var project = new Project() { ProjectId = 1, ProjectName = "Project 1", StatusCode = "T" };
			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(project);
			var service = new TicketsService(ticketRepoMock.Object, projectServiceMock.Object);

			//	Act
			var result = await service.DeleteAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}