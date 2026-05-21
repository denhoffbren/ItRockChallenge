using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
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

            builder.Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.Completed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.Active)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();
        }
    }
}
