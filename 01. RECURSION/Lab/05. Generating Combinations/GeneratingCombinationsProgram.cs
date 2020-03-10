namespace _05._Generating_Combinations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GeneratingCombinationsProgram
    {
        public static void Main()
        {
            var elements = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var k = int.Parse(Console.ReadLine());

            var result = GetCombinations(elements, k)
                .Select(x => string.Join(" ", x));

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static List<int[]> GetCombinations(int[] elements, int k)
        {
            var combos = new List<int[]>();
            var currentVector = new int[k];

            GenerateCombinations(elements, currentVector, 0, 0, combos);

            return combos;
        }

        private static void GenerateCombinations(int[] elements, int[] currentVector, int index, int border, List<int[]> combos)
        {
            if (index == currentVector.Length)
            {
                var clone = new int[currentVector.Length];
                Array.Copy(currentVector, clone, currentVector.Length);
                combos.Add(clone);
            }
            else
            {
                for (var i = border; i < elements.Length; i++)
                {
                    currentVector[index] = elements[i];
                    GenerateCombinations(elements, currentVector, index + 1, i + 1, combos);
                }
            }
        }
    }
}