using FluentValidation;
using TaskTrackerApp.Application.CQRS.TaskItems.Commands.Request;

namespace TaskTrackerApp.Application.Validators.TaskItems;

public class CreateTaskItemRequestValidator : AbstractValidator<CreateTaskItemCommandRequest>
{
    public CreateTaskItemRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
