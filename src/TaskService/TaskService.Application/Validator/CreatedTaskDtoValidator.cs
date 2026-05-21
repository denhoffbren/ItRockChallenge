using FluentValidation;
using TaskService.Application.DTOs;

namespace TaskService.Application.Validator
{
    public class CreatedTaskDtoValidator : AbstractValidator<CreatedTaskDto>
    {
        public CreatedTaskDtoValidator()
        {
            RuleFor(u => u.Title).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(u => u.Description).MaximumLength(100);
        }
    }
}
