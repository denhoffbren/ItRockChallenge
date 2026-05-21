using TaskService.Domain.Entities;

namespace TaskService.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task CreateTask(Tarea task);
        Task DeleteTask(Tarea task);
        Task<List<Tarea>> GetTasksByUserId(string idUser, int? pageNumber, int? pageSize);
        Task UpdateTask(Tarea task);
        Task<Tarea?> GetTaskById(Guid id);
    }
}
