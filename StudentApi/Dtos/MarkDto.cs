using System;
namespace StudentApi.Dtos
{
	public class MarkDto
	{
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal ExamMark { get; set; }
        public decimal AssignmentMark { get; set; }
    }
}

