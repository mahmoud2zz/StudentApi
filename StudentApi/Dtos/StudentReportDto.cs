using System;
namespace StudentApi.Dtos
{
	public class StudentReportDto
	{
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public List<ClassMarkDto> Classes { get; set; } = new();
        public decimal OverallAverage { get; set; }
    }
}

