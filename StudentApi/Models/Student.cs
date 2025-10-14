using System;
namespace StudentApi.Models
{
	public class Student
	{
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }

    }
}

