namespace _02._Processor_Scheduling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ProcessorSchedulingProgram
    {
        private static int _count;

        public static void Main()
        {
            var tasks = ReadTasks()
                .OrderByDescending(x => x.Value)
                .ToList();

            var max = tasks.Select(x => x.Deadline)
                .OrderByDescending(x => x).First();

            var result = new List<Task>();

            for (var i = 1; i <= max; i++)
            {
                var current = tasks
                    .FirstOrDefault();

                if (current == null)
                {
                    break;
                }

                tasks.Remove(current);
                result.Add(current);
                //tasks.RemoveAll(x => x.Deadline == i);
            }

            result = result.OrderBy(x => x.Deadline).ToList();
            Console.WriteLine($"Optimal schedule: {string.Join(" -> ", result.Select(x => x.Number))}");
            Console.WriteLine($"Total value: {result.Sum(x => x.Value)}");
        }

        private static List<Task> ReadTasks()
        {
            _count = int.Parse(Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            var tasks = new List<Task>();

            for (var i = 0; i < _count; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var value = tokens[0];
                var deadline = tokens[1];
                var number = i + 1;

                var task = new Task(number, value, deadline);
                tasks.Add(task);
            }

            return tasks;
        }
    }
}
