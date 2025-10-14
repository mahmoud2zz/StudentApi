using System.Collections.Concurrent;
using StudentApi.Models;
using StudentApi.Services.Students;
using StudentApi.Services.Classe;

namespace StudentApi.Services.Enrollments
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ConcurrentDictionary<(int studentId, int classId), Enrollment> _enrollments = new();
        private int _nextId = 1;

        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        public EnrollmentService(IStudentService studentService, IClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }

        public Enrollment? AddEnrollment(int studentId, int classId)
        {
            var student = _studentService.GetStudentById(studentId);
            var classObj = _classService.GetClassById(classId);

            if (student == null || classObj == null)
                return null;

            // تحقق إذا كان الطالب مسجل بالفعل
            if (_enrollments.ContainsKey((studentId, classId)))
                return null;

            var enrollment = new Enrollment
            {
                Id = _nextId++,
                StudentId = studentId,
                ClassId = classId,
                EnrollmentDate = DateTime.UtcNow
            };

            _enrollments.TryAdd((studentId, classId), enrollment);
            return enrollment;
        }

        public bool IsStudentEnrolled(int studentId, int classId)
        {
            return _enrollments.ContainsKey((studentId, classId));
        }

        public List<Enrollment> GetAllEnrollments() => _enrollments.Values.ToList();
    }
}
