using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Services;
using Tickets.Web.Controllers;
using Xunit;

namespace Tickets.Web.Tests
{
    public class ProjectsControllerTests
    {

		[Fact]
		public async void GetAsync()
		{
			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

			var controller = new ProjectsController(projectServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void GetAsyncNull()
		{
			//	Arrange
			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
			var controller = new ProjectsController(projectServiceMock.Object);

			//	Act
			var result = await controller.GetAsync(1);

			//	Assert
			Assert.Null(result);
		}

		[Fact]
		public async void AddAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.AddAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var controller = new ProjectsController(projectServiceMock.Object);

			//	Act
			var result = await controller.PostAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.EditAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var controller = new ProjectsController(projectServiceMock.Object);

			//	Act
			var result = await controller.PutAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void DeleteAsync()
		{

			//	Arrange
			var expected = new Project() { ProjectId = 5, ProjectName = "Project 1", StatusCode = "T" };

			var projectServiceMock = new Mock<IProjectsService>();
			projectServiceMock.Setup(i => i.DeleteAsync(It.IsAny<Project>())).ReturnsAsync((Project x) => x);
			var controller = new ProjectsController(projectServiceMock.Object);

			//	Act
			var result = await controller.DeleteAsync(expected);

			//	Assert
			Assert.Equal(expected, result);
		}
	}
}