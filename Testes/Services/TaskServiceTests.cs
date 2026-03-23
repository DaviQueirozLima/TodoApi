using Moq;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;

namespace TodoApi.Tests.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task Create()
        {
            var repoMock = new Mock<ITaskRepository>();
            var service = new TaskService(repoMock.Object);

            var dto = new CreateTaskDto
            {
                Title = "Teste",
                Description = "Descrição"
            };

            var result = await service.Create(dto);

            Assert.NotNull(result);
            Assert.Equal("Teste", result.Title);

            repoMock.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Once);
            repoMock.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task GetAll()
        {
            var repoMock = new Mock<ITaskRepository>();

            repoMock.Setup(r => r.GetAll()).ReturnsAsync(new List<TaskItem>
            {
                new TaskItem { Title = "Task 1" },
                new TaskItem { Title = "Task 2" }
            });

            var service = new TaskService(repoMock.Object);

            var result = await service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetById()
        {
            var repoMock = new Mock<ITaskRepository>();

            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Teste" };

            repoMock.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);

            var service = new TaskService(repoMock.Object);

            var result = await service.GetById(task.Id);

            Assert.NotNull(result);
            Assert.Equal("Teste", result.Title);
        }

        [Fact]
        public async Task Update()
        {
            var repoMock = new Mock<ITaskRepository>();

            var task = new TaskItem { Id = Guid.NewGuid() };

            repoMock.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);

            var service = new TaskService(repoMock.Object);

            var dto = new UpdateTaskDto
            {
                Title = "Atualizado",
                Description = "Desc",
                IsCompleted = true
            };

            var result = await service.Update(task.Id, dto);

            Assert.True(result);

            repoMock.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Once);
            repoMock.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Delete()
        {
            var repoMock = new Mock<ITaskRepository>();

            var task = new TaskItem { Id = Guid.NewGuid() };

            repoMock.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);

            var service = new TaskService(repoMock.Object);

            var result = await service.Delete(task.Id);

            Assert.True(result);

            repoMock.Verify(r => r.Delete(It.IsAny<TaskItem>()), Times.Once);
            repoMock.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}