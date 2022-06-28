namespace _06._Merge_Sort;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();

    public static void Main()
    {
        ReadInput();

        array = MergeSort(array);

        Console.WriteLine(string.Join(" ", array));
    }

    private static void ReadInput()
        => array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    // Memory: O(n*log(n))
    private static int[] MergeSort(int[] elements)
    {
        if (elements.Length == 1)
        {
            return elements;
        }

        var middleIndex = elements.Length / 2;

        var leftHalf = elements.Take(middleIndex).ToArray();
        var rightHalf = elements.Skip(middleIndex).ToArray();

        return MergeArrays(MergeSort(leftHalf), MergeSort(rightHalf));
    }

    public static int[] MergeArrays(int[] left, int[] right)
    {
        var sorted = new int[left.Length + right.Length];

        var sortedIndex = 0; 
        var leftIndex = 0; 
        var rightIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            if (left[leftIndex] < right[rightIndex])
            {
                sorted[sortedIndex++] = left[leftIndex++];
            }
            else
            {
                sorted[sortedIndex++] = right[rightIndex++];
            }
        }

        while (leftIndex < left.Length)
        {
            sorted[sortedIndex++] = left[leftIndex++];
        }

        while (rightIndex < right.Length)
        {
            sorted[sortedIndex++] = right[rightIndex++];
        }

        return sorted;
    }
}
