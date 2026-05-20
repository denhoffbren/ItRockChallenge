using TaskService.Application.DTOs;

namespace TaskService.Application.Interface
{
    public interface ITaskUsesCases
    {
        Task<List<TaskResponseDto>> GetAllTask(string idUser);
        Task<TaskResponseDto> CreateTask(string idUser, CreatedTaskDto createdTaskDto);
        Task<TaskResponseDto> UpdateTask(string idUser, Guid idTask, UpdatedTaskDto updatedTaskDto);
        Task DeleteTask(string idUser, Guid idTask); 
    }
}
