using FluentValidation;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;

namespace TaskTrackerApp.Application.Validators.TaskItems;

public class UpdateTaskItemRequestValidator : AbstractValidator<UpdateTaskItemCommandRequest>
{
    public UpdateTaskItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
