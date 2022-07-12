namespace _01._Two_Minutes_to_Midnight;

using System;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");
        var k = int.Parse(Console.ReadLine() ?? "0");

        var result = GetBinomCoefficient(n, k);

        Console.WriteLine(result);
    }

    private static BigInteger GetBinomCoefficient(int n, int k)
    {
        BigInteger result = 1;

        if (k > n)
        {
            return 0;
        }

        for (var i = 1; i <= k; i++)
        {
            result *= n--;
            result /= i;
        }

        return result;
    }
}
