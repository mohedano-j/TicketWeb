using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Services;
using Tickets.Web.Controllers;
using Xunit;

namespace Tickets.Web.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public async Task SearchAsyncPartialId()
        {
			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Doe" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"}
			};

			var employeeServiceMock = new Mock<IEmployeesService>();
			employeeServiceMock.Setup(i => i.SearchAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var controller = new EmployeesController(employeeServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("5");

			//	Assert
			Assert.Equal(expected, result);

			employeeServiceMock.Verify(s => s.SearchAsync(5));
			employeeServiceMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task SearchAsyncByName()
		{
			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Smith" },
				new Employee { EmployeeId = 50, FirstName = "Mary", LastName="Smith"}
			};

			var employeeServiceMock = new Mock<IEmployeesService>();
			employeeServiceMock.Setup(i => i.SearchAsync(It.IsAny<string>())).ReturnsAsync(expected);

			var controller = new EmployeesController(employeeServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("Smith");

			//	Assert
			Assert.Equal(expected, result);

			employeeServiceMock.Verify(s => s.SearchAsync("Smith"));
			employeeServiceMock.VerifyNoOtherCalls();
		}


		[Fact]
		public async Task SearchAsyncByFirstNameAndLastName()
		{
			//	Arrange
			var expected = new List<Employee>() {
				new Employee{ EmployeeId = 5, FirstName = "Joe", LastName = "Smith" }
			};

			var employeeServiceMock = new Mock<IEmployeesService>();
			employeeServiceMock.Setup(i => i.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);

			var controller = new EmployeesController(employeeServiceMock.Object);

			//Act
			var result = await controller.SearchAsync("Joe Smith");

			//	Assert
			Assert.Single(result);

			employeeServiceMock.Verify(s => s.SearchAsync("Joe", "Smith"));
			employeeServiceMock.VerifyNoOtherCalls();
		}
	}
}