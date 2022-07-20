namespace _01._Super_Set;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int[] elements;

    static Program()
    {
        elements = Array.Empty<int>();
    }

    public static void Main()
    {
        ReadInput();

        for (var i = 1; i <= elements.Length; i++)
        {
            var slots = new int[i];

            GenerateCombinations(0, 0, slots);
        }
    }

    private static void ReadInput()
        => elements = (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void GenerateCombinations(int index, int start, int[] slots)
    {
        if (index >= slots.Length)
        {
            Print(slots);
        }
        else
        {
            for (var i = start; i < elements.Length; i++)
            {
                slots[index] = elements[i];
                GenerateCombinations(index + 1, i + 1, slots);
            }
        }
    }

    private static void Print(IEnumerable<int> slots) 
        => Console.WriteLine(string.Join(" ", slots));
}
