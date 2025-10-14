using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Enrollments;

namespace StudentApi.Endpoints.Enrollments;

public class EnrollStudentEndpoint : Endpoint<EnrollmentDto, Response<Enrollment>>
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollStudentEndpoint(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void Configure()
    {
        Post("/api/enrollments");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EnrollmentDto req, CancellationToken ct)
    {
        var enrollment = _enrollmentService.AddEnrollment(req.StudentId, req.ClassId);

        if (enrollment == null)
        {
            await SendAsync(ResponseBuilder.Failure<Enrollment>(
                "Invalid enrollment request. Either student/class not found or already enrolled."),
                400, ct);
            return;
        }

        await SendAsync(ResponseBuilder.Success("Enrollment created successfully", enrollment), 200, ct);
    }
}
