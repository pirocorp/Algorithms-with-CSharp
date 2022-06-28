namespace _05._School_Teams;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static string[] girls = Array.Empty<string>();
    private static string[] boys = Array.Empty<string>();

    private static readonly List<string[]> girlsCombinations = new List<string[]>();
    private static readonly List<string[]> boysCombinations = new List<string[]>();

    private static string[] combination = Array.Empty<string>();

    public static void Main(string[] args)
    {
        ReadInput();

        CalculateGirlsCombinations(3);
        CalculateBoysCombinations(2);

        GenerateOutput();
    }

    private static void ReadInput()
    {
        girls = ReadLineOfElementsFromConsole();

        boys = ReadLineOfElementsFromConsole();
    }

    private static string[] ReadLineOfElementsFromConsole()
    {
        return (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();
    }

    private static void CalculateGirlsCombinations(int count)
    {
        combination = new string[count];

        Combinations(0, 0, girls, girlsCombinations);
    }

    private static void CalculateBoysCombinations(int count)
    {
        combination = new string[count];

        Combinations(0, 0, boys, boysCombinations);
    }

    private static void Combinations(int index, int start, string[] elements, List<string[]> results)
    {
        if (index >= combination.Length)
        {
            results.Add(combination.ToArray());
        }
        else
        {
            for (var i = start; i < elements.Length; i++)
            {
                combination[index] = elements[i];
                Combinations(index + 1, i + 1, elements, results);
            }
        }
    }

    private static void GenerateOutput()
    {
        foreach (var girlsCombination in girlsCombinations)
        {
            foreach (var boysCombination in boysCombinations)
            {
                Console.WriteLine(string.Join(", ", girlsCombination.Concat(boysCombination)));
            }
        }
    }
}
