using System;
using System.Collections.Concurrent;
using StudentApi.Models;

namespace StudentApi.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly ConcurrentDictionary<int, Student> _students = new();
        private int _nextId = 1;

        public Student AddStudent(Student student)
        {
            student.Id = _nextId++;
            _students[student.Id] = student;
            return student;
        }



        public List<Student> GetAllStudents(string? search = null, int page = 1, int pageSize = 10)
        {
            var query = _students.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Age.ToString() == search);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Student? GetStudentById(int id)
        {
            _students.TryGetValue(id, out var student);
            return student;
        }


        public void UpdateStudent(Student student)
        {
            if (_students.ContainsKey(student.Id))
            {
                _students[student.Id] = student;
            }
        }



        public bool DeleteStudent(int id)
        {
            return _students.TryRemove(id, out _);

        }
    }





}

