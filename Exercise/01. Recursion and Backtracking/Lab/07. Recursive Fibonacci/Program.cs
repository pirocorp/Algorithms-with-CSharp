namespace _07._Recursive_Fibonacci;

using System;
using System.Collections.Generic;

public class Program
{
    private static readonly Dictionary<int, long> fibonacci = new Dictionary<int, long>()
    {
        {0, 1},
        {1, 1},
    };

    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        var fib = GetFibonacci(n);

        Console.WriteLine(fib);
    }

    private static long GetFibonacci(int i)
    {
        if (fibonacci.ContainsKey(i))
        {
            return fibonacci[i];
        }

        fibonacci[i] = GetFibonacci(i - 1) + GetFibonacci(i - 2);

        return fibonacci[i];
    }
}
