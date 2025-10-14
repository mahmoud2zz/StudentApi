using FluentValidation;
using StudentApi.Dtos;

namespace StudentApi.Validators
{
    public class MarkValidator : AbstractValidator<MarkDto>
    {
        public MarkValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("StudentId is required");
            RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("ClassId is required");
            RuleFor(x => x.ExamMark).InclusiveBetween(0, 100).WithMessage("Exam mark must be between 0 and 100");
            RuleFor(x => x.AssignmentMark).InclusiveBetween(0, 100).WithMessage("Assignment mark must be between 0 and 100");
        }
    }
}
