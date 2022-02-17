using Moq;
using System.Collections.Generic;
using Tickets.Services.Data;
using Xunit;

namespace Tickets.Services.Tests
{
	public class StatusServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new List<Status>() { new Status { StatusCode = "T", StatusDesc= "To Do" },
				new Status { StatusCode = "D", StatusDesc= "Done" } };

			var statusRepoMock = new Mock<IStatusRepository>();
			statusRepoMock.Setup(s => s.GetAsync()).ReturnsAsync(expected);

			var service = new StatusService(statusRepoMock.Object);

			//	Act
			var result = await service.GetAsync();

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var statusRepoMock = new Mock<IStatusRepository>();
			statusRepoMock.Setup(i => i.GetAsync()).ReturnsAsync(value: null);
			var service = new StatusService(statusRepoMock.Object);

			//	Act
			var result = await service.GetAsync();

            //	Assert
            Assert.Null(result);
		}
	}
}