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

        public async Task CreateTasks(List<Tarea> tasks)
        {
            applicationDbContext.Tasks.AddRange(tasks);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(Tarea task)
        {
            task.Active = false;
            applicationDbContext.Tasks.Update(task);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Tarea>> GetTasksByUserId(string idUser, int? pageNumber, int? pageSize)
        {
            if (pageNumber is null && pageSize is null)
            {
                return await applicationDbContext.Tasks.Where(p => p.UserId == idUser && p.Active).ToListAsync();
            }
            return await applicationDbContext.Tasks
                .Where(p => p.UserId == idUser && p.Active)
                .OrderBy(p => p.Id)
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync();
        }

        public async Task<Tarea?> GetTaskById(Guid id)
        {
            return await applicationDbContext.Tasks.SingleOrDefaultAsync(p => p.Id == id && p.Active);
        }

        public async Task UpdateTask(Tarea task)
        {
            applicationDbContext.Tasks.Update(task);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Tarea>> GetPagedByUserId(int pageNumber, int pageSize, string idUser)
        {
            return await applicationDbContext.Tasks
                .Where(p => p.UserId == idUser && p.Active)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
