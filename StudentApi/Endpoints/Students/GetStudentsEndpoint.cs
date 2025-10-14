using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Endpoints.Students;

public class GetStudentsEndpoint : Endpoint<GetStudentsRequest>
{
    private readonly IStudentService _studentService;

    public GetStudentsEndpoint(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public override void Configure()
    {
        Get("/api/students");
        AllowAnonymous();
    }

    public override async Task HandleAsync(  GetStudentsRequest req, CancellationToken ct)
    {
        var students = _studentService.GetAllStudents(req.Search, req.Page, req.PageSize);
        await SendAsync(ResponseBuilder.Success("Students retrieved successfully", students), 200, ct);
    }
}
