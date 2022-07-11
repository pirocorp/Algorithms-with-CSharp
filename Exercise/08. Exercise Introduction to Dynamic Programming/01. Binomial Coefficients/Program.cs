namespace _01._Binomial_Coefficients;

using System;

public static class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");
        var k = int.Parse(Console.ReadLine() ?? "0");

        var result = GetBinomCoefficient(n, k);

        Console.WriteLine(result);
    }

    private static long GetBinomCoefficient(int n, int k)
    {
        long result = 1;

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
