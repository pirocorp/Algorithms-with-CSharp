namespace _01._Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BlocksProgram
    {
        private static HashSet<char> GenerateSetOfFirstLetters(int n)
        {
            var set = new HashSet<char>();

            for (var i = 0; i < n; i++)
            {
                set.Add((char)('A' + i));
            }

            return set;
        }

        private static void Variations(int currentCellIndex, char[] currentVariation, List<string> variations, bool[] used, char[] elements)
        {
            if (currentCellIndex == currentVariation.Length)
            {
                variations.Add(new string(currentVariation));
            }
            else
            {
                for (var i = 0; i < elements.Length; i++)
                {
                    if (!used[i])
                    {
                        var currentElement = elements[i];
                        used[i] = true;

                        currentVariation[currentCellIndex] = currentElement;
                        Variations(currentCellIndex + 1, currentVariation, variations, used, elements);

                        used[i] = false;
                    }
                }
            }
        }

        private static string RotateVariation(string variation)
        {
            variation = variation.Insert(0, $"{variation[2]}");
            variation = variation.Remove(3, 1);

            var chars = variation.ToCharArray();
            var tmp = chars[2];
            chars[2] = chars[3];
            chars[3] = tmp;

            return new string(chars);
        }

        private static List<string> GetAllVariationsOfSet(int n)
        {
            var elements = GenerateSetOfFirstLetters(n).ToArray();
            var used = new bool[elements.Length];
            var variations = new List<string>();
            var currentVariation = new char[4];

            Variations(0, currentVariation, variations, used, elements);

            var uniqueVariations = new List<string>();
            var existingVariations = new HashSet<string>();

            foreach (var variation in variations)
            {
                if (!existingVariations.Contains(variation))
                {
                    uniqueVariations.Add(variation);
                    existingVariations.Add(variation);

                    var rotatedVariation = variation;

                    for (var i = 0; i < 3; i++)
                    {
                        rotatedVariation = RotateVariation(rotatedVariation);
                        existingVariations.Add(rotatedVariation);
                    }
                }
            }

            return uniqueVariations;
        }

        private static void NonOptimalSolution(int n)
        {
            var variations = GetAllVariationsOfSet(n);

            Console.WriteLine($"Number of blocks: {variations.Count}");

            foreach (var variation in variations)
            {
                Console.WriteLine(variation);
            }
        }

        private static void MoreOptimalSolution(int n)
        {
            var lastLetter = 'A' + n - 1;

            var totalCount = (n - 3) * (n - 2) * (n - 1) * n / 4;
            Console.WriteLine($"Number of blocks: {totalCount}");

            for (var l1 = 'A'; l1 <= lastLetter; l1++)
            {
                for (var l2 = (char)(l1 + 1); l2 <= lastLetter; l2++)
                {
                    for (var l3 = (char)(l1 + 1); l3 <= lastLetter; l3++)
                    {
                        if (l3 != l2)
                        {
                            for (var l4 = (char)(l1 + 1); l4 <= lastLetter; l4++)
                            {
                                if (l4 != l3 && l4 != l2)
                                {
                                    Console.WriteLine($"{l1}{l2}{l3}{l4}");
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            //NonOptimalSolution(n);

            MoreOptimalSolution(n);
        }
    }
}
