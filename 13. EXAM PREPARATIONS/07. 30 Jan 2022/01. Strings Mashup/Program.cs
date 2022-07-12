namespace _01._Strings_Mashup;

using System;
using System.Linq;

public static class Program
{
    private static char[] elements = Array.Empty<char>();

    private static char[] slots = Array.Empty<char>();

    public static void Main()
    {
        elements = (Console.ReadLine() ?? string.Empty)
            .ToCharArray()
            .OrderBy(x => x)
            .ToArray();

        var n = int.Parse(Console.ReadLine() ?? "0");
        slots = new char[n];

        Combinations(0, 0);
    }

    static void Combinations(int index, int start)
    {
        if (index >= slots.Length)
        {
            Console.WriteLine(string.Join(string.Empty, slots));
        }
        else
        {
            for (var i = start; i < elements.Length; i++)
            {
                slots[index] = elements[i];
                Combinations(index + 1, i);
            }
        }
    }
}
