namespace _05._QuickSort;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        ReadInput();

        QuickSort(array);

        Console.WriteLine(string.Join(" ", array));
    }

    private static void QuickSort(int[] elements)
        => QuickSortHelper(elements, 0, elements.Length - 1);

    public static void QuickSortHelper(int[] elements, int startIndex, int endIndex)
    {
        if (startIndex >= endIndex)
        {
            return;
        }
            
        var pivotIndex = startIndex;

        var leftIndex = startIndex + 1;
        var rightIndex = endIndex;

        while (leftIndex <= rightIndex)
        {
            if (array[leftIndex] > array[pivotIndex] &&
                array[rightIndex] < array[pivotIndex])
            {
                (elements[rightIndex], elements[leftIndex]) = (elements[leftIndex], elements[rightIndex]);
            }

            if (array[leftIndex] <= array[pivotIndex])
            {
                leftIndex += 1;
            }

            if (array[rightIndex] >= array[pivotIndex])
            {
                rightIndex -= 1;
            }

        }

        (elements[rightIndex], elements[pivotIndex]) = (elements[pivotIndex], elements[rightIndex]);

        var isLeftSubArraysSmaller = rightIndex - 1 - startIndex < endIndex - (rightIndex + 1);

        if (isLeftSubArraysSmaller)
        {
            QuickSortHelper(array, startIndex, rightIndex - 1);
            QuickSortHelper(array, rightIndex + 1, endIndex);
        }
        else
        {
            QuickSortHelper(array, rightIndex + 1, endIndex);
            QuickSortHelper(array, startIndex, rightIndex - 1);
        }
    }

    private static void ReadInput()
        => array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();
}
