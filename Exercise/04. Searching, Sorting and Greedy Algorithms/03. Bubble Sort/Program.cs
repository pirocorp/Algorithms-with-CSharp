namespace _03._Bubble_Sort;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        ReadInput();

        BubbleSort(array);

        Console.WriteLine(string.Join(" ", array));
    }

    private static void ReadInput()
        => array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void BubbleSort(int[] elements)
    {
        for (var offset = 0; offset < elements.Length; offset++)
        {
            var end = elements.Length - offset - 1;

            for (var i = 0; i < end; i++)
            {
                var first = elements[i];
                var second = elements[i + 1];

                if (second < first)
                {
                    (elements[i], elements[i + 1]) = (elements[i + 1], elements[i]);
                }
            }
        }
    }
}
