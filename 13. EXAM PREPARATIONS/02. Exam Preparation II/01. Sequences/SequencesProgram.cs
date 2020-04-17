namespace _01._Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SequencesProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var result = new StringBuilder();

            Generate(n, 0, new List<int>(), result);

            Console.WriteLine(result);
        }

        private static void Generate(int target, int index, List<int> currentCombination, StringBuilder result)
        {
            if (currentCombination.Sum() > target)
            {
                return;
            }

            result.AppendLine(string.Join(" ", currentCombination));

            for (var i = 1; i <= target; i++)
            {
                currentCombination.Add(i);
                Generate(target, index + 1, currentCombination, result);

                if (currentCombination.Sum() >= target)
                {
                    currentCombination.RemoveAt(index);
                    break;
                }

                currentCombination.RemoveAt(index);
            }
        }
    }
}
