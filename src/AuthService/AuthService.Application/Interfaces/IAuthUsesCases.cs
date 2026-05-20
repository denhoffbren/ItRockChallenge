using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface IAuthUsesCases
    {
        Task SignUp(SignUpUserDto signUpUserDto);
        Task<string> SignIn(SignUpUserDto signUpUserDto);
    }
}
