namespace _04._Variations_with_Repetition;

using System;
using System.Linq;

public static class Program
{
    private static string[] elements = Array.Empty<string>();
    private static string[] variation = Array.Empty<string>();

    public static void Main(string[] args)
    {
        elements = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray() ?? Array.Empty<string>();


        var n = int.Parse(Console.ReadLine() ?? "0");

        variation = new string[n];

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
            elements
                .ToList()
                .ForEach(element =>
                {
                    variation[index] = element;
                    Variations(index + 1);
                });
        }
    }

    private static void Print() => Console.WriteLine(string.Join(" ", variation));
}
