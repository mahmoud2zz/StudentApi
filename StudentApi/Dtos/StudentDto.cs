using System;
namespace StudentApi.Dtos
{
	public class StudentDto
	{
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}

