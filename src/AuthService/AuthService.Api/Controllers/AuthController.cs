using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUsesCases authUsesCases;

        public AuthController(IAuthUsesCases authUsesCases)
        {
            this.authUsesCases = authUsesCases;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto signUpUserDto)
        {
            await authUsesCases.SignUp(signUpUserDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignUpUserDto signUpUserDto)
        {
            string response = await authUsesCases.SignIn(signUpUserDto);
            return new JsonResult(response);
        }
    }
}
