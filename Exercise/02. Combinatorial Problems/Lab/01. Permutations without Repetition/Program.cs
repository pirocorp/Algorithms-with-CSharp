namespace _01._Permutations_without_Repetition;

using System;
using System.Linq;

public static class Program
{
    private static string[] elements = Array.Empty<string>();
    private static string[] permutation = Array.Empty<string>();
    private static bool[] used = Array.Empty<bool>();

    public static void Main()
    {
        elements = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray() ?? Array.Empty<string>();

        permutation = new string[elements.Length];
        used = new bool[elements.Length];

        Permute(0);
    }

    private static void Permute(int index)
    {
        if (index >= permutation.Length)
        {
            Print();
        }
        else
        {
            for (var i = 0; i < elements.Length; i++)
            {
                if (used[i])
                {
                    continue;
                }

                used[i] = true;
                permutation[index] = elements[i];
                Permute(index + 1);
                used[i] = false;
            }
        }
    }

    private static void Print() => Console.WriteLine(string.Join(" ", permutation));
}
