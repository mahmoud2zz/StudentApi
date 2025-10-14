using System;
namespace StudentApi.Models;

	public interface IStudentService
	{
     Student  AddStudent(Student student);

    List<Student> GetAllStudents(string? search = null, int page = 1, int pageSize = 10);

    Student? GetStudentById(int id);
    void UpdateStudent(Student student);
    bool DeleteStudent(int id);

}

