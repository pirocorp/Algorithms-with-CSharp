namespace _01._Binary_Search;

using System;
using System.Linq;

public static class Program
{
    private static int[] array = Array.Empty<int>();
    private static int target;

    public static void Main()
    {
        ReadInput();

        var index = BinarySearch(array, target);

        Console.WriteLine(index);
    }

    private static void ReadInput()
    {
        array = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

        target = int.Parse(Console.ReadLine() ?? "0");
    }

    private static int BinarySearch(int[] elements, int item)
    {
        var left = 0;
        var right = elements.Length - 1;

        while (left <= right)
        {
            var mid = (left + right) / 2;
            var element = elements[mid];

            if (element == item)
            {
                return mid;
            }

            if (item > element)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }
}
