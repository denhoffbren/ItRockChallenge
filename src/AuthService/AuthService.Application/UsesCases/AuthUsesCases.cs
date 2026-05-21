using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.UsesCases
{
    public class AuthUsesCases : IAuthUsesCases
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IJwtService jwtService;
        private readonly IMapper mapper;

        public AuthUsesCases(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }

        public async Task<UserTokenDto> SignIn(UserDto signUpUserDto)
        {
            User? user = await userRepository.GetByUsuario(signUpUserDto.Usuario);
            if (user is null)
            {
                throw new UsuarioInexistenteException();
            }

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.Password, signUpUserDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new CredencialesInvalidasException();
            }
            return new() { Usuario = user.Usuario, Token = jwtService.GenerateToken(user) };
        }

        public async Task SignUp(UserDto signUpUserDto)
        {
            User? user = await userRepository.GetByUsuario(signUpUserDto.Usuario);
            if (user is not null)
            {
                throw new UsuarioRegistradoException();
            }
            User newUser = mapper.Map<User>(signUpUserDto);
            newUser.Password = passwordHasher.HashPassword(user, newUser.Password);
            await userRepository.CreateUser(newUser);
        }
    }
}
