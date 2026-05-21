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

        /// <summary>
        /// Crea un nuevo usuario en el sistema. 
        /// </summary>
        /// <param name="userDto">Datos del usuario a crear</param>
        /// <returns>Usuario creado</returns>
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserDto userDto)
        {
            await authUsesCases.SignUp(userDto);
            return Ok("Usuario creado");
        }

        /// <summary>
        /// Permite ingresar al sistema
        /// </summary>
        /// <param name="userDto">Credenciales del usuario</param>
        /// <returns>Usuario creado</returns>
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] UserDto userDto)
        {
            UserTokenDto response = await authUsesCases.SignIn(userDto);
            return new JsonResult(response);
        }
    }
}
