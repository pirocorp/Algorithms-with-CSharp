namespace _01._School_Teams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SchoolTeamsProgram
    {
        private static void GenerateCombinations(int index, int start, int k, string[] elements, string[] currentCombination, List<string[]> allCombinations)
        {
            if (index >= k)
            {
                allCombinations.Add(currentCombination.ToArray());
            }
            else
                for (var i = start; i < elements.Length; i++)
                {
                    currentCombination[index] = elements[i];
                    GenerateCombinations(index + 1, i + 1, k, elements, currentCombination, allCombinations);
                }
        }


        public static void Main()
        {
            var girls = Console.ReadLine()
                .Split(", ");

            var boys = Console.ReadLine()
                .Split(", ");

            var allGirlsCombinations = new List<string[]>();
            GenerateCombinations(0, 0, 3, girls, new string[3], allGirlsCombinations);

            var allBoysCombinations = new List<string[]>();
            GenerateCombinations(0, 0, 2, boys, new string[2], allBoysCombinations);

            foreach (var girlsCombination in allGirlsCombinations)
            {
                foreach (var boysCombination in allBoysCombinations)
                {
                    var comb = new List<string>(girlsCombination);
                    comb.AddRange(boysCombination);

                    Console.WriteLine(string.Join(", ", comb));
                }
            }
        }
    }
}
