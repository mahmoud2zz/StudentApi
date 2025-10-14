using System;
using StudentApi.Models;

namespace StudentApi.Models
{
	public interface IMarkService
    {
        Mark AddMark(Mark mark);
        List<Mark> GetMarks();
        decimal? CalculateAverageMarksForClass(int classId);
    }
}

