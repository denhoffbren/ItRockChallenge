using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.IdUser);

            builder.Property(p => p.IdUser)
                .UseIdentityColumn();

            builder.Property(p => p.Usuario)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
