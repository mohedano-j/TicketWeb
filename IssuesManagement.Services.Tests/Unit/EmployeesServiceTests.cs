using Moq;
using System.Collections.Generic;
using Tickets.Services.Data;
using Xunit;

namespace Tickets.Services.Tests
{
	public class EmployeesServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Employee() { EmployeeId = 5, FirstName = "Joe", LastName = "Doe" };

			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

            //	Assert
            Assert.Null(result);
		}

		[Fact]
		public async void SearchAsyncByPartialId()
		{

			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"}
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync(5);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByPartialIdEmpty()
		{

			//	Arrange
			var expected = new List<Employee>() {
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync(5);

			//	Assert
			Assert.Empty(result);
		}

		[Fact]
		public async void SearchAsyncByName()
		{

			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Doe"}
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Doe");

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByNameEmpty()
		{

			//	Arrange
			var expected = new List<Employee>()
			{
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Doe");

			//	Assert
			Assert.Empty(result);
		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastName()
		{

			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Joe","Doe");

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void SearchAsyncByFirstNameAndLastNameEmpty()
		{

			//	Arrange
			var expected = new List<Employee>()
			{
			};
			var employeeRepoMock = new Mock<IEmployeeRepository>();
			employeeRepoMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);
			var service = new EmployeesService(employeeRepoMock.Object);

			//	Act
			var result = await service.SearchAsync("Joe","Doe");

			//	Assert
			Assert.Empty(result);
		}
	}
}