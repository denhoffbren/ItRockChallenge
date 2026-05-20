using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskService.Application.Interface;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }

    }
}
