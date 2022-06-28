namespace _07._Merge_Sort_Memory_Optimized;

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

    // Memory: O(n)
    private static int[] MergeSort(int[] elements)
    {
        if (elements.Length <= 1)
        {
            return elements;
        }

        var copy = new int[elements.Length];
        Array.Copy(elements, copy, elements.Length);

        MergeSortHelper(elements, copy, 0, elements.Length - 1);

        return elements;
    }

    public static void MergeSortHelper(int[] source, int[] copy, int leftIndex, int rightIndex)
    {
        if (leftIndex >= rightIndex)
        {
            return;
        }

        var middleIndex = (leftIndex + rightIndex) / 2;

        MergeSortHelper(copy, source, leftIndex, middleIndex);
        MergeSortHelper(copy, source, middleIndex + 1, rightIndex);

        MergeArrays(source, copy, leftIndex, middleIndex, rightIndex);
    }

    public static void MergeArrays(int[] source, int[] copy, int startIndex, int middleIndex, int endIndex)
    {
        var sourceIndex = startIndex;
        var leftIndex = startIndex; 
        var rightIndex = middleIndex + 1;

        while (leftIndex <= middleIndex && rightIndex <= endIndex)
        {
            if (copy[leftIndex] < copy[rightIndex])
                source[sourceIndex++] = copy[leftIndex++];
            else
                source[sourceIndex++] = copy[rightIndex++];
        }

        while (leftIndex <= middleIndex)
        {
            source[sourceIndex++] = copy[leftIndex++];
        }

        while (rightIndex <= endIndex)
        {
            source[sourceIndex++] = copy[rightIndex++];
        }
    }
}
