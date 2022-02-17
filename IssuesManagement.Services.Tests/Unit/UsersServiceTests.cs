using Moq;
using System.Collections.Generic;
using IssuesManagement.Services.Data;
using Xunit;

namespace IssuesManagement.Services.Tests
{
	public class UsersServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new User() { UserId = 5, FirstName = "Joe", LastName = "Doe" };

			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

            //	Assert
            Assert.Null(result);
		}

		[Fact]
		public async void SearchAsyncByPartialId()
		{

			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, FirstName = "Mary", LastName="Smith"}
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync(5);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByPartialIdEmpty()
		{

			//	Arrange
			var expected = new List<User>() {
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync(5);

			//	Assert
			Assert.Empty(result);
		}

		[Fact]
		public async void SearchAsyncByName()
		{

			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, FirstName = "Joe", LastName = "Doe" },
				new User { UserId = 50, FirstName = "Mary", LastName="Doe"}
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Doe");

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByNameEmpty()
		{

			//	Arrange
			var expected = new List<User>()
			{
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Doe");

			//	Assert
			Assert.Empty(result);
		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastName()
		{

			//	Arrange
			var expected = new List<User>() {
				new User{ UserId = 5, FirstName = "Joe", LastName = "Doe" },
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Joe","Doe");

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastNameEmpty()
		{

			//	Arrange
			var expected = new List<User>()
			{
			};
			var userRepoMock = new Mock<IUsersRepository>();
			userRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);
			var service = new UsersService(userRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Joe","Doe");

			//	Assert
			Assert.Empty(result);
		}
	}
}