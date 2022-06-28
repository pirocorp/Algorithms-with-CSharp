namespace _04._Insertion_Sort;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        ReadInput();

        InsertionSort(array);

        Console.WriteLine(string.Join(" ", array));
    }

    private static void ReadInput()
        => array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void InsertionSort(int[] elements)
    {
        for (var index = 1; index < elements.Length; index++)
        {
            var currentIndex = index;

            while (currentIndex > 0 && elements[currentIndex] < elements[currentIndex - 1])
            {
                (elements[currentIndex], elements[currentIndex - 1]) = (elements[currentIndex - 1], elements[currentIndex]);
                currentIndex--;
            }
        }

    }
}
