using TodoApi.Models;

namespace TodoApi.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem?> GetById(Guid id);
        Task<TaskItem> Create(TaskItem task);
        Task<bool> Update(Guid id, TaskItem updatedTask);
        Task<bool> Delete(Guid id);
    }
}
