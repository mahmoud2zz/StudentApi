using FastEndpoints;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Students;

namespace StudentApi.Endpoints.Students;

public class DeleteStudentEndpoint : EndpointWithoutRequest
{
    private readonly IStudentService _studentService;

    public DeleteStudentEndpoint(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public override void Configure()
    {
        Delete("/api/students/{id:int}"); // ID من الـ URL
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var student = _studentService.GetStudentById(id);
        if (student == null)
        {
            await SendAsync(ResponseBuilder.Failure<Student>("Student not found"), 404, ct);
            return;
        }

        _studentService.DeleteStudent(id);
        await SendAsync(ResponseBuilder.Success("Student deleted successfully", student), 200, ct);
    }
}
