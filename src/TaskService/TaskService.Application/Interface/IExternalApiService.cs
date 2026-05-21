using TaskService.Application.DTOs;

namespace TaskService.Application.Interface
{
    public interface IExternalApiService
    {
        Task<List<ExternalTaskDto>> GetTasks();
    }
}
