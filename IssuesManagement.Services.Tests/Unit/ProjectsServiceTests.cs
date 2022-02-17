using Moq;
using System.Collections.Generic;
using Tickets.Services.Data;
using Xunit;

namespace Tickets.Services.Tests
{
	public class ProjectsServiceTests
	{
		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectRepoMock = new Mock<IProjectRepository>();
			projectRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var service = new ProjectsService(projectRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
            //	Arrange
            var projectRepoMock = new Mock<IProjectRepository>();
			projectRepoMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var service = new ProjectsService(projectRepoMock.Object);

			//	Act
			var result = await service.GetAsync(1);

            //	Assert
            Assert.Null(result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectRepoMock = new Mock<IProjectRepository>();
			projectRepoMock.Setup(i => i.AddAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var service = new ProjectsService(projectRepoMock.Object);

			//	Act
			var result = await service.AddAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectRepoMock = new Mock<IProjectRepository>();
			projectRepoMock.Setup(i => i.EditAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var service = new ProjectsService(projectRepoMock.Object);

			//	Act
			var result = await service.EditAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void DeleteAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectRepoMock = new Mock<IProjectRepository>();
			projectRepoMock.Setup(i => i.DeleteAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var service = new ProjectsService(projectRepoMock.Object);

			//	Act
			var result = await service.DeleteAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}