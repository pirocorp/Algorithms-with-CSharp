namespace _01._Reverse_Array;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        array = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();

        ICollection<int> result = new List<int>();

        Reverse(0, result);

        Console.WriteLine(string.Join(" ", result));
    }

    private static void Reverse(int index, ICollection<int> result)
    {
        if (index < array.Length - 1)
        {
            Reverse(index + 1, result);
        }

        result.Add(array[index]);
    }
}
