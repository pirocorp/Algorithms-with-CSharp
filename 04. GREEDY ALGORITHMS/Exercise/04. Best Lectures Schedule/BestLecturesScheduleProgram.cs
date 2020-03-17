namespace _04._Best_Lectures_Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BestLecturesScheduleProgram
    {
        private static int _count;

        public static void Main()
        {
            var lectures = ReadLectures();

            var result = new List<Lecture>();

            while (lectures.Count > 0)
            {
                var current = lectures
                    .OrderBy(x => x.EndTime)
                    .First();

                result.Add(current);
                lectures = lectures
                    .Where(x => x.StartTime >= current.EndTime)
                    .ToList();
            }

            Console.WriteLine($"Lectures ({result.Count}):");
            foreach (var lecture in result)
            {
                Console.WriteLine($"{lecture.StartTime}-{lecture.EndTime} -> {lecture.Name}");
            }
        }

        private static List<Lecture> ReadLectures()
        {
            var lectures = new List<Lecture>();

            _count = int.Parse(Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            for (var i = 0; i < _count; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var lectureName = tokens[0];

                var timeTokens = tokens[1]
                    .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var start = timeTokens[0];
                var end = timeTokens[1];

                var lecture = new Lecture(lectureName, start, end);

                lectures.Add(lecture);
            }

            return lectures;
        }
    }
}
