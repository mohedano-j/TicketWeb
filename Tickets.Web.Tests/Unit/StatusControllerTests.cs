using Moq;
using System.Collections.Generic;
using Tickets.Services;
using Tickets.Web.Controllers;
using Xunit;

namespace Tickets.Web.Tests
{
	public class StatusControllerTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new List<Status>() { new Status { StatusCode = "T", StatusDesc= "To Do" },
				new Status { StatusCode = "D", StatusDesc= "Done" } };

			var statusServiceMock = new Mock<IStatusService>();
			statusServiceMock.Setup(s => s.GetAsync()).ReturnsAsync(expected);

			var controller = new StatusController(statusServiceMock.Object);

			//	Act
			var result = await controller.GetAsync();

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var statusServiceMock = new Mock<IStatusService>();
			statusServiceMock.Setup(i => i.GetAsync()).ReturnsAsync(value: null);
			var controller = new StatusController(statusServiceMock.Object);

			//	Act
			var result = await controller.GetAsync();

            //	Assert
            Assert.Null(result);
		}
	}
}