using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IssuesManagement.Services;
using IssuesManagement.Web.Controllers;
using Xunit;

namespace IssuesManagement.Web.Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task SearchAsyncPartialId()
        {
			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, UserName = "AAA", FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, UserName = "BBB", FirstName = "Mary", LastName="Smith"}
			};

			var userServiceMock = new Mock<IUsersService>();
			userServiceMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var controller = new UsersController(userServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("5");

			//	Assert
			Assert.Equal(expected, result);

			userServiceMock.Verify(s => s.SearchAsync(5));
			userServiceMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task SearchAsyncByName()
		{
			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, UserName = "AAA", FirstName = "Joe", LastName = "Smith" },
				new User { UserId = 50, UserName = "BBB", FirstName = "Mary", LastName="Smith"}
			};

			var userServiceMock = new Mock<IUsersService>();
			userServiceMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);

			var controller = new UsersController(userServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("Smith");

			//	Assert
			Assert.Equal(expected, result);

			userServiceMock.Verify(s => s.SearchAsync("Smith"));
			userServiceMock.VerifyNoOtherCalls();
		}


		[Fact]
		public async Task SearchAsyncByFirstNameAndLastName()
		{
			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, UserName = "AAA", FirstName = "Joe", LastName = "Smith" }
			};

			var userServiceMock = new Mock<IUsersService>();
			userServiceMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);

			var controller = new UsersController(userServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("Joe Smith");

			//	Assert
			Assert.Single(result);

			userServiceMock.Verify(s => s.SearchAsync("Joe", "Smith"));
			userServiceMock.VerifyNoOtherCalls();
		}
	}
}