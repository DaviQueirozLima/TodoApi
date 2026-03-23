using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Service;

namespace TodoApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAll()
        {
            var tasks = await _repository.GetAll();

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TaskResponseDto?> GetById(Guid id)
        {
            var t = await _repository.GetById(id);
            if (t == null) return null;

            return new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            };
        }

        public async Task<TaskResponseDto> Create(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description
            };

            await _repository.Add(task);
            await _repository.SaveChanges();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<bool> Update(Guid id, UpdateTaskDto dto)
        {
            var task = await _repository.GetById(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;

            await _repository.Update(task);
            await _repository.SaveChanges();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var task = await _repository.GetById(id);
            if (task == null) return false;

            await _repository.Delete(task);
            await _repository.SaveChanges();

            return true;
        }
    }
}