using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem?> GetById(Guid id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Add(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
        }

        public async Task Update(TaskItem task)
        {
            _context.TaskItems.Update(task);
        }

        public async Task Delete(TaskItem task)
        {
            task.DeletedAt = DateTime.UtcNow;
            _context.TaskItems.Update(task);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}