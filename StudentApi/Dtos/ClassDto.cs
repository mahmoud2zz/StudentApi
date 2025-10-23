using System;
using FluentValidation;

namespace StudentApi.Dtos
{
	public class ClassDto
	{
      
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

