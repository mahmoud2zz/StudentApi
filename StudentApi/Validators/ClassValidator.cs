using FluentValidation;
using StudentApi.Dtos;

namespace StudentApi.Validators;

public class ClassValidator : AbstractValidator<ClassDto>
{
    public ClassValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Class name is required");
        RuleFor(x => x.Teacher).NotEmpty().WithMessage("Teacher name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
    }
}
