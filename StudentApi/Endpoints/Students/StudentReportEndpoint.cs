using FastEndpoints;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Services.Students;
using StudentApi.Services.Enrollments;
using StudentApi.Services.Marks;
using StudentApi.Dtos;

namespace StudentApi.Endpoints.Students;

public class StudentReportEndpoint : EndpointWithoutRequest
{
    private readonly IStudentService _studentService;
    private readonly IClassService _classService;
    private readonly IEnrollmentService _enrollmentService;
    private readonly IMarkService _markService;

    public StudentReportEndpoint(
        IStudentService studentService,
        IClassService classService,
        IEnrollmentService enrollmentService,
        IMarkService markService)
    {
        _studentService = studentService;
        _classService = classService;
        _enrollmentService = enrollmentService;
        _markService = markService;
    }

    public override void Configure()
    {
        Get("/api/students/{studentId}/report");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var studentId = Route<int>("studentId");

        var student = _studentService.GetStudentById(studentId);
        if (student == null)
        {
            await SendAsync(ResponseBuilder.Failure<StudentReportDto>("Student not found"), 404, ct);
            return;
        }

        var enrollments = _enrollmentService.GetAllEnrollments()
                            .Where(e => e.StudentId == studentId)
                            .ToList();

        if (!enrollments.Any())
        {
            await SendAsync(ResponseBuilder.Failure<StudentReportDto>("Student is not enrolled in any classes"), 404, ct);
            return;
        }

        var report = new StudentReportDto
        {
            StudentId = student.Id,
            StudentName = $"{student.FirstName} {student.LastName}"
        };

        foreach (var enrollment in enrollments)
        {
            var classObj = _classService.GetClassById(enrollment.ClassId);
            var mark = _markService.GetMarks()
                        .FirstOrDefault(m => m.StudentId == studentId && m.ClassId == enrollment.ClassId);

            report.Classes.Add(new ClassMarkDto
            {
                ClassId = classObj!.Id,
                ClassName = classObj.Name,
                ExamMark = mark?.ExamMark ?? 0,
                AssignmentMark = mark?.AssignmentMark ?? 0
            });
        }

        report.OverallAverage = report.Classes.Any()
            ? report.Classes.Average(c => c.TotalMark)
            : 0;

        await SendAsync(ResponseBuilder.Success("Student report generated successfully", report), 200, ct);
    }
}
