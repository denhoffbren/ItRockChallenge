using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Entities;
using TaskService.Domain.Repositories;
using TaskService.Infrastructure.Persistence.Context;

namespace TaskService.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task CreateTask(Tarea task)
        {
            applicationDbContext.Tasks.Add(task);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(Tarea task)
        {
            applicationDbContext.Tasks.Remove(task);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Tarea>> GetMyTask(string idUser)
        {
            return await applicationDbContext.Tasks.Where(p => p.UserId == idUser).ToListAsync();
        }

        public async Task<Tarea?> GetTaskById(Guid id)
        {
            return await applicationDbContext.Tasks.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateTask(Tarea task)
        {
            applicationDbContext.Tasks.Update(task);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
