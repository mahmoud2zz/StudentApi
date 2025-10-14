using System;
namespace StudentApi.Models
{
	public class Class
	{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Teacher { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}

