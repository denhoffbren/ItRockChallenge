using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Entities;

namespace TaskService.Infrastructure.Persistence
{
    public class TaskConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Tittle)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Completed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();
        }
    }
}
