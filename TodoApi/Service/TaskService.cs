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

        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TaskItem?> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TaskItem> Create(TaskItem task)
        {
            await _repository.Add(task);
            await _repository.SaveChanges();
            return task;
        }

        public async Task<bool> Update(Guid id, TaskItem updatedTask)
        {
            var task = await _repository.GetById(id);
            if (task == null) return false;

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
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