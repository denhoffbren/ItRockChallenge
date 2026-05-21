using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskService.Application.DTOs;
using TaskService.Application.Interface;
using TaskService.Infrastructure.Security;

namespace TaskService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskUsesCases taskUsesCases;

        public TasksController(ITaskUsesCases taskUsesCases)
        {
            this.taskUsesCases = taskUsesCases;
        }

        /// <summary>
        /// Obtiene todos los registros del usuario logueado.
        /// </summary>
        /// <returns>Tareas encontradas</returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="200">Ok</response>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, int? pageSize)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            List<TaskResponseDto> listTask = await taskUsesCases.GetAllTask(userId, pageNumber, pageSize);
            return Ok(new ApiResponse<List<TaskResponseDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Tareas obtenidas correctamente",
                Data = listTask
            });
        }

        /// <summary>
        /// Crea una tarea relacionada al usuario logueado.
        /// </summary>
        /// <returns>Tarea creada correctamente</returns>
        /// <param name="createdTaskDto">Datos de la tarea a crear</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<TaskResponseDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreatedTaskDto createdTaskDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            TaskResponseDto result = await taskUsesCases.CreateTask(userId, createdTaskDto);
            return Ok(new ApiResponse<TaskResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Tarea creada correctamente",
                Data = result
            });
        }

        /// <summary>
        /// Actualiza una tarea.
        /// </summary>
        /// <returns>Tarea actualizada correctamente</returns>
        /// <param name="idTask">ID de la tarea a actualizar</param>
        /// <param name="updatedTaskDto">Datos de la tarea</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<TaskResponseDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPatch("{idTask}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid idTask, [FromBody] UpdatedTaskDto updatedTaskDto)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            TaskResponseDto result = await taskUsesCases.UpdateTask(userId, idTask, updatedTaskDto);
            return Ok(new ApiResponse<TaskResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Tarea actualizada correctamente",
                Data = result
            });
        }

        /// <summary>
        /// Elimina una tarea.
        /// </summary>
        /// <returns>Tarea eliminada correctamente</returns>
        /// <param name="idTask">ID de la tarea a eliminar</param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<TaskResponseDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{idTask}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid idTask)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await taskUsesCases.DeleteTask(userId, idTask);
            return Ok(new ApiResponse<TaskResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Tarea eliminada correctamente",
            });
        }

        /// <summary>
        /// Importa 5 tareas al usuario logueado.
        /// </summary>
        /// <returns>La importación finalizo correctamente.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<TaskResponseDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("import")]
        public async Task<IActionResult> ImportTask()
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await taskUsesCases.ImportTask(userId);
            return Ok(new ApiResponse<Task>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "La importación finalizo correctamente."
            });
        }
    }
}
