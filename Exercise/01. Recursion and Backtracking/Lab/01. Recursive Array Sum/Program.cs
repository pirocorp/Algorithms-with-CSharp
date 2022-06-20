namespace _01._Recursive_Array_Sum;

using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        var arr = Console.ReadLine()
            ?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x.Trim()))
            .ToArray()
            ?? Array.Empty<int>();

        Console.WriteLine(GetSum(arr, 0));
    }

    public static int GetSum(int[] arr, int index)
    {
        if (index >= arr.Length)
        {
            return 0;
        }

        return arr[index] + GetSum(arr, index + 1);
    }
}
