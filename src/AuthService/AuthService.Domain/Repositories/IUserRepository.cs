using AuthService.Domain.Entities;

namespace AuthService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User?> GetByUsuario(string usuario);
    }
}
