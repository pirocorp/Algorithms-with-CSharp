namespace _03._Variations_without_Repetition;

using System;
using System.Linq;

public static class Program
{
    private static string[] elements = Array.Empty<string>();
    private static string[] variation = Array.Empty<string>();
    private static bool[] used = Array.Empty<bool>();

    public static void Main(string[] args)
    {
        elements = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray() ?? Array.Empty<string>();


        var n = int.Parse(Console.ReadLine() ?? "0");

        variation = new string[n];
        used = new bool[elements.Length];

        Variations(0);
    }

    private static void Variations(int index)
    {
        if (index >= variation.Length)
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
                variation[index] = elements[i];
                Variations(index + 1);
                used[i] = false;
            }
        }
    }

    private static void Print() => Console.WriteLine(string.Join(" ", variation));
}
