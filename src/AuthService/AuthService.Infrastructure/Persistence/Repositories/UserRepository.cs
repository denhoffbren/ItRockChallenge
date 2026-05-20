using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task CreateUser(User user)
        {
            applicationDbContext.Users.Add(user);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByUsuario(string usuario)
        {
            return await applicationDbContext.Users.FirstOrDefaultAsync(p => p.Usuario == usuario);
        }
    }
}
