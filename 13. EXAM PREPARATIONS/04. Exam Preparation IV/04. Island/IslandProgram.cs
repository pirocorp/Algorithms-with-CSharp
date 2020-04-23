namespace _04._Island
{
    using System;
    using System.Linq;

    public static class IslandProgram
    {
        private static void SlowSolution(int maxNumber, int[] numbers)
        {
            var max = 0;

            for (var current = 1; current <= maxNumber; current++)
            {
                var currentMax = 0;

                for (var i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] >= current)
                    {
                        currentMax += current;
                    }
                    else
                    {
                        if (currentMax > max)
                        {
                            max = currentMax;
                        }

                        currentMax = 0;
                    }
                }

                if (currentMax > max)
                {
                    max = currentMax;
                }
            }

            Console.WriteLine(max);
        }

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var maxNumber = numbers.Max();

            //SlowSolution(maxNumber, numbers);
        }
    }
}
