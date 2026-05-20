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
        private readonly IMapper mapper;

        public TaskUsesCases(ITaskRepository taskRepository,
            IMapper mapper)
        {
            this.taskRepository = taskRepository;
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
                throw new NoTienePermisosExcepcion();
            }
            return task;
        }

        public async Task<TaskResponseDto> CreateTask(string idUser, CreatedTaskDto createdTaskDto)
        {
            Tarea task = mapper.Map<Tarea>(createdTaskDto); 
            task.UserId = idUser;
            task.CreatedAt = DateTime.Now;
            await taskRepository.CreateTask(task);

            return mapper.Map<TaskResponseDto>(task);
        }

        public async Task DeleteTask(string idUser, Guid idTask)
        {
            Tarea task = ValidarTask(idUser, idTask).Result;
            await taskRepository.DeleteTask(task);
        }

        public async Task<List<TaskResponseDto>> GetAllTask(string idUser)
        {
            List<Tarea> listTask = await taskRepository.GetMyTask(idUser);
            return mapper.Map<List<TaskResponseDto>>(listTask);
        }

        public async Task<TaskResponseDto> UpdateTask(string idUser, Guid idTask, UpdatedTaskDto updatedTaskDto)
        {
            Tarea task = ValidarTask(idUser, idTask).Result;
            task.Title = updatedTaskDto.Title;
            task.Description = updatedTaskDto.Description;
            task.Completed = updatedTaskDto.Completed;

            await taskRepository.UpdateTask(task);
            return mapper.Map<TaskResponseDto>(task);
        }
    }
}
