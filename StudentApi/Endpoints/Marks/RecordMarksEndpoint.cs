using FastEndpoints;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Services.Classe;
using StudentApi.Services.Students;
using StudentApi.Services.Marks;
using StudentApi.Services.Enrollments;

namespace StudentApi.Endpoints.Marks
{
    public class RecordMarksEndpoint : Endpoint<MarkDto>
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMarkService _markService;

        public RecordMarksEndpoint(
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
            Post("/api/marks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(MarkDto req, CancellationToken ct)
        {
            var student = _studentService.GetStudentById(req.StudentId);
            if (student == null)
            {
                await SendAsync(ResponseBuilder.Failure<Mark>("Student not found"), 404, ct);
                return;
            }

            var classObj = _classService.GetClassById(req.ClassId);
            if (classObj == null)
            {
                await SendAsync(ResponseBuilder.Failure<Mark>("Class not found"), 404, ct);
                return;
            }

            if (!_enrollmentService.IsStudentEnrolled(req.StudentId, req.ClassId))
            {
                await SendAsync(ResponseBuilder.Failure<Mark>("Student is not enrolled in this class"), 400, ct);
                return;
            }

            var mark = new Mark
            {
                StudentId = req.StudentId,
                ClassId = req.ClassId,
                ExamMark = req.ExamMark,
                AssignmentMark = req.AssignmentMark,
                 RecordedAt= DateTime.UtcNow
            };

            var result = _markService.AddMark(mark);

            await SendAsync(ResponseBuilder.Success("Marks recorded successfully", result), 200, ct);
        }
    }
}
