using System;
namespace StudentApi.Dtos
{
	public class ClassMarkDto
	{
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public decimal ExamMark { get; set; }
        public decimal AssignmentMark { get; set; }
        public decimal TotalMark => ExamMark + AssignmentMark;
    }
}

