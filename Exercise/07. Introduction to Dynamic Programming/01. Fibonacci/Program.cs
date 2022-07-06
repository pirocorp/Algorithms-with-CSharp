namespace _01._Fibonacci;

using System;

public static class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        var solution = new long[n + 1];

        solution[0] = 0;
        solution[1] = 1;

        for (var i = 2; i <= n; i++)
        {
            solution[i] = solution[i - 1] + solution[i - 2];
        }

        Console.WriteLine(solution[n]);
    }
}
