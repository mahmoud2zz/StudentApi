using StudentApi.Models;

namespace StudentApi.Services.Enrollments
{
    public interface IEnrollmentService
    {
        Enrollment? AddEnrollment(int studentId, int classId);
        List<Enrollment> GetAllEnrollments();
        bool IsStudentEnrolled(int studentId, int classId);

    }
}
