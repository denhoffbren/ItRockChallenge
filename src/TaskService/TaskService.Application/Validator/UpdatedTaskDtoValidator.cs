using FluentValidation;
using TaskService.Application.DTOs;

namespace TaskService.Application.Validator
{
    public class UpdatedTaskDtoValidator : AbstractValidator<UpdatedTaskDto>
    {
        public UpdatedTaskDtoValidator()
        {
            RuleFor(u => u.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(u => u.Description).MaximumLength(200);
            RuleFor(u => u.Completed).NotNull().NotEmpty();
        }
    }
}
