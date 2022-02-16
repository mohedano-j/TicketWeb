using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Services;
using Tickets.Web.Controllers;
using Xunit;

namespace Tickets.Web.Tests
{
    public class TicketsControllerTests
    {

		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var projectServiceMock = new Mock<ITicketsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var controller = new TicketsController(projectServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
			//	Arrange
			var projectServiceMock = new Mock<ITicketsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var controller = new TicketsController(projectServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Null(result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var projectServiceMock = new Mock<ITicketsService>();
			projectServiceMock.Setup(i => i.AddAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);
			var controller = new TicketsController(projectServiceMock.Object);

			//	Act
			var result = await controller.PostAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var projectServiceMock = new Mock<ITicketsService>();
			projectServiceMock.Setup(i => i.EditAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);
			var controller = new TicketsController(projectServiceMock.Object);

			//	Act
			var result = await controller.PutAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void DeleteAsync()
		{

			//	Arrange
			var expected = new Ticket() { TicketId = 5, Title = "Ticket 1", StatusCode = "T", ProjectId = 1 };

			var projectServiceMock = new Mock<ITicketsService>();
			projectServiceMock.Setup(i => i.DeleteAsync(It.IsAny<Ticket>())).ReturnsAsync((Ticket x) => x);
			var controller = new TicketsController(projectServiceMock.Object);

			//	Act
			var result = await controller.DeleteAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}