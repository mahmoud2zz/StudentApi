using FluentValidation;
using StudentApi.Dtos;
using StudentApi.Models;

namespace StudentApi.Validators;

public class StudentValidator : AbstractValidator<StudentDto>
{
    public StudentValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be positive");
    }
}
