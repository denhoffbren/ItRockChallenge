using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface IAuthUsesCases
    {
        Task SignUp(UserDto signUpUserDto);
        Task<UserTokenDto> SignIn(UserDto signUpUserDto);
    }
}
