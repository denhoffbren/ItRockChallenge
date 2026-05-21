using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Application.Interface;
using TaskService.Domain.Entities;
using TaskService.Domain.Exceptions;
using TaskService.Domain.Repositories;

namespace TaskService.Application.UsesCases
{
    public class TaskUsesCases : ITaskUsesCases
    {
        private readonly ITaskRepository taskRepository;
        private readonly IExternalApiService externalApiService;
        private readonly IMapper mapper;

        public TaskUsesCases(ITaskRepository taskRepository,
            IExternalApiService externalApiService,
            IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.externalApiService = externalApiService;
            this.mapper = mapper;
        }

        private async Task<Tarea> ValidarTask(string idUser, Guid idTask)
        {
            Tarea? task = await taskRepository.GetTaskById(idTask);
            if (task is null)
            {
                throw new TaskNoEncontradaException();
            }

            if (task.UserId != idUser)
            {
                throw new NoTienePermisosException();
            }
            return task;
        }

        public async Task<TaskResponseDto> CreateTask(string idUser, CreatedTaskDto createdTaskDto)
        {
            Tarea task = mapper.Map<Tarea>(createdTaskDto); 
            task.UserId = idUser;
            task.CreatedAt = DateTime.UtcNow;
            await taskRepository.CreateTask(task);

            return mapper.Map<TaskResponseDto>(task);
        }

        public async Task DeleteTask(string idUser, Guid idTask)
        {
            Tarea task = await ValidarTask(idUser, idTask);
            await taskRepository.DeleteTask(task);
        }

        public async Task<List<TaskResponseDto>> GetAllTask(string idUser, int? pageNumber, int? pageSize)
        {
            List<Tarea> listTask = await taskRepository.GetTasksByUserId(idUser, pageNumber, pageSize);
            return mapper.Map<List<TaskResponseDto>>(listTask);
        }

        public async Task<TaskResponseDto> UpdateTask(string idUser, Guid idTask, UpdatedTaskDto updatedTaskDto)
        {
            Tarea task = await ValidarTask(idUser, idTask);
            task.Title = updatedTaskDto.Title;
            task.Description = updatedTaskDto.Description;
            task.Completed = updatedTaskDto.Completed;

            await taskRepository.UpdateTask(task);
            return mapper.Map<TaskResponseDto>(task);
        }

        public async Task ImportTask(string idUser)
        {
            List<ExternalTaskDto> tasksExternal = (await externalApiService.GetTasks())
                .Where(p => p.userId == Convert.ToInt32(idUser))
                .Take(5)
                .ToList();

            foreach (ExternalTaskDto externalTask in tasksExternal)
            {
                Tarea task = new()
                {
                    Title = externalTask.title,
                    Completed = externalTask.completed,
                    UserId = externalTask.userId.ToString(),
                    CreatedAt = DateTime.UtcNow
                };
                await taskRepository.CreateTask(task);
            }
        }
    }
}
