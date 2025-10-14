using System.Collections.Concurrent;
using StudentApi.Models;

namespace StudentApi.Services.Marks
{
    public class MarkService : IMarkService
    {
        private readonly ConcurrentDictionary<int, Mark> _marks = new();
        private int _nextId = 1;

        public Mark AddMark(Mark mark)
        {
            mark.Id = _nextId++;
            _marks[mark.Id] = mark;
            return mark;
        }

        public List<Mark> GetMarks() => _marks.Values.ToList();

        public decimal? CalculateAverageMarksForClass(int classId)
        {
            
            var classMarks = _marks.Values
                                   .Where(m => m.ClassId == classId)
                                   .ToList();

            if (!classMarks.Any())
                return null;

            return classMarks.Average(m => m.ExamMark + m.AssignmentMark);
        }



    }
}
