using Moq;
using System.Collections.Generic;
using IssuesManagement.Services.Data;
using Xunit;

namespace IssuesManagement.Services.Tests
{
	public class IssuesServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueRepoMock = new Mock<IIssuesRepository>();
			issueRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var service = new IssuesService(issueRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var issueRepoMock = new Mock<IIssuesRepository>();
			issueRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var service = new IssuesService(issueRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

            //	Assert
            Assert.Null(result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueRepoMock = new Mock<IIssuesRepository>();
			issueRepoMock.Setup(i => i.AddAsync(It.IsAny<Issue>())).ReturnsAsync((Issue x) => x);
			var service = new IssuesService(issueRepoMock.Object);

			//	Act
			var result = await service.AddAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Issue() { IssueId = 5, Title = "Issue 1", StatusOpened = true };

			var issueRepoMock = new Mock<IIssuesRepository>();
			issueRepoMock.Setup(i => i.EditAsync(It.IsAny<Issue>())).ReturnsAsync((Issue x) => x);
			var service = new IssuesService(issueRepoMock.Object);

			//	Act
			var result = await service.EditAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}