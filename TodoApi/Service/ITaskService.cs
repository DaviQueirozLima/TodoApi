using TodoApi.DTOs;

namespace TodoApi.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAll();
        Task<TaskResponseDto?> GetById(Guid id);
        Task<TaskResponseDto> Create(CreateTaskDto dto);
        Task<bool> Update(Guid id, UpdateTaskDto dto);
        Task<bool> Delete(Guid id);
    }
}
