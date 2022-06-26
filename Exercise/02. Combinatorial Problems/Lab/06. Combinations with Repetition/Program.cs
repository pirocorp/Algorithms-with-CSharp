namespace _06._Combinations_with_Repetition;

using System;
using System.Linq;

public static class Program
{
    private static string[] elements = Array.Empty<string>();
    private static string[] combination = Array.Empty<string>();

    public static void Main(string[] args)
    {
        elements = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray() ?? Array.Empty<string>();


        var n = int.Parse(Console.ReadLine() ?? "0");

        combination = new string[n];

        Combinations(0, 0);
    }

    static void Combinations(int index, int start)
    {
        if (index >= combination.Length)
        {
            Print();
        }
        else
        {
            for (var i = start; i < elements.Length; i++)
            {
                combination[index] = elements[i];
                Combinations(index + 1, i);
            }
        }
    }

    private static void Print() => Console.WriteLine(string.Join(" ", combination));
}
