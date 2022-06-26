namespace _02._Nested_Loops;

using System;
using System.Linq;

public static class Program
{
    private static int[] counters = Array.Empty<int>();
    private static int[] lengths = Array.Empty<int>();

    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        counters = new int[n];
        lengths = Enumerable.Repeat(n, n).ToArray();

        NestedLoops(0);
    }

    private static void NestedLoops(int level)
    {
        if (level == counters.Length)
        {
            Print();
        }
        else
        {
            for (counters[level] = 0; counters[level] < lengths[level]; counters[level]++)
            {
                NestedLoops(level + 1);
            }
        }
    }

    private static void Print()
        => Console.WriteLine(string.Join(" ", counters.Select(x => x + 1)));
}
