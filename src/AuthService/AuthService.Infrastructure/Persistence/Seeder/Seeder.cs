using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Persistence.Seeder
{
    public class Seeder
    {
        private readonly IPasswordHasher<User> passwordHasher;

        public Seeder(IPasswordHasher<User> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {

                User admin = new()
                {
                    Usuario = "admin"
                };
                admin.Password = passwordHasher.HashPassword(admin, "password123");
                db.Users.Add(admin);
                await db.SaveChangesAsync();
            }
        }
    }
}
