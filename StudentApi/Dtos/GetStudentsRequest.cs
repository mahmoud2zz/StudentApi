using System;
namespace StudentApi.Dtos
{
	public class GetStudentsRequest
    {
        
        public string? Search { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

