using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem?> GetById(Guid id);
        Task Add(TaskItem task);
        Task Update(TaskItem task);
        Task Delete(TaskItem task);
        Task SaveChanges();
    }
}
