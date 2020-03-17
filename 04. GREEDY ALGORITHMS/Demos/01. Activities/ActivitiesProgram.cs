namespace _01._Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ActivitiesProgram
    {
        public static void Main()
        {
            var activities = GetActivities()
                .OrderBy(a => a.EndTime)
                .ToList();

            var last = activities.First();

            Console.WriteLine($"{last.StartTime} - {last.EndTime}");

            for (var i = 1; i < activities.Count; i++)
            {
                var current = activities[i];

                if (current.StartTime >= last.EndTime)
                {
                    last = current;
                    Console.WriteLine($"{last.StartTime} - {last.EndTime}");
                }
            }
        }

        private static IEnumerable<Activity> GetActivities()
        {
            var startingTimes = new[]
            {
                1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12
            };

            var endingTimes = new[]
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14
            };

            var activities = new List<Activity>();


            for (var i = 0; i < startingTimes.Length; i++)
            {
                var start = startingTimes[i];
                var end = endingTimes[i];

                activities.Add(new Activity(start, end));
            }

            return activities;
        }
    }
}
