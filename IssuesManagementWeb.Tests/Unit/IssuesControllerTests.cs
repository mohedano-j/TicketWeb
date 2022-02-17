using IssuesManagement.Services;
using IssuesManagement.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IssuesManagement.Web.Tests
{
    public class IssuesControllerTests
    {

		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueServiceMock = new Mock<IIssuesService>();
			issueServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var controller = new IssuesController(issueServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
			//	Arrange
			var issueServiceMock = new Mock<IIssuesService>();
			issueServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var controller = new IssuesController(issueServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Null(result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueServiceMock = new Mock<IIssuesService>();
			issueServiceMock.Setup(i => i.AddAsync(It.IsAny<Issue>())).ReturnsAsync((Issue x) => x);
			var controller = new IssuesController(issueServiceMock.Object);

			//	Act
			var result = await controller.PostAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueServiceMock = new Mock<IIssuesService>();
			issueServiceMock.Setup(i => i.EditAsync(It.IsAny<Issue>())).ReturnsAsync((Issue x) => x);
			var controller = new IssuesController(issueServiceMock.Object);

			//	Act
			var result = await controller.PutAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}