using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Validators;

namespace StudentApi.Endpoints.Students;

public class UpdateStudentEndpoint : Endpoint<StudentDto, Response<Student>>
{
    private readonly IStudentService _studentService;

    public UpdateStudentEndpoint(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public override void Configure()
    {
        Put("/api/students/{id:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(StudentDto req, CancellationToken ct)
    {
        var id = Route<int>("id");

        var existingStudent = _studentService.GetStudentById(id);
        if (existingStudent == null)
        {
            await SendAsync(ResponseBuilder.Failure<Student>("Student not found"), 404, ct);
            return;
        }

        var validator = new StudentValidator();
        var validationResult = validator.Validate(req);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            await SendAsync(ResponseBuilder.Failure<Student>(errors), 400, ct);
            return;
        }

        existingStudent.FirstName = req.FirstName;
        existingStudent.LastName = req.LastName;
        existingStudent.Age = req.Age;

        _studentService.UpdateStudent(existingStudent);

        await SendAsync(ResponseBuilder.Success("Student updated successfully", existingStudent), 200, ct);
    }
}
