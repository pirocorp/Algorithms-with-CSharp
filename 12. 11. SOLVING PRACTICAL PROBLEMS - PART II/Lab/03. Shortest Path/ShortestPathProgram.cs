namespace _03._Shortest_Path
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ShortestPathProgram
    {
        private static readonly List<string> _set = new List<string>{ "L", "R", "S" };

        private static void GenerateVariations(int count, List<string> variations, string currentVariation)
        {
            if (currentVariation.Length == count)
            {
                variations.Add(currentVariation);
                return;
            }

            foreach (var element in _set)
            {
                GenerateVariations(count, variations, currentVariation + element);
            }
        }

        private static List<string> ReplaceStarsWithVariationChars(List<string> variations, string inputString)
        {
            var result = new List<string>();

            for (var i = 0; i < variations.Count; i++)
            {
                var variation = variations[i];
                var currentString = inputString.ToList();

                var index = currentString.IndexOf('*');
                var varIndex = 0;

                while (index != -1)
                {
                    currentString[index] = variation[varIndex];
                    index = currentString.IndexOf('*');
                    varIndex++;
                }

                result.Add(new string(currentString.ToArray()));
            }

            return result;
        }

        private static void MySolution()
        {
            var inputString = Console.ReadLine();

            var count = inputString.Count(x => x == '*');

            var variations = new List<string>();
            GenerateVariations(count, variations, string.Empty);

            var result = ReplaceStarsWithVariationChars(variations, inputString);

            result.Sort();
            Console.WriteLine(result.Count);
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        public static void Main()
        {
            MySolution();
        }
    }
}