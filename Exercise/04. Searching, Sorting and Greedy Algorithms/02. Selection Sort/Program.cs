namespace _02._Selection_Sort;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        ReadInput();

        SelectionSort(array);

        Console.WriteLine(string.Join(" ", array));
    }

    private static void SelectionSort(int[] elements)
    {
        for (var index = 0; index < elements.Length; index++)
        {
            var minElement = int.MaxValue;
            var minIndex = -1;

            for (var min = index; min < elements.Length; min++)
            {
                if (elements[min] < minElement)
                {
                    minElement = elements[min];
                    minIndex = min;
                }
            }

            (elements[index], elements[minIndex]) = (elements[minIndex], elements[index]);
        }
    }

    private static void ReadInput()
        => array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();
}
