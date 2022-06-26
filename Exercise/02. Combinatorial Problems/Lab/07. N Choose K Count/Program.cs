namespace _07._N_Choose_K_Count;

using System;

public static class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");
        var k = int.Parse(Console.ReadLine() ?? "0");

        var result = Binom(n, k);
        Console.WriteLine(result);
    }

    static long Binom(int n, int k)
    {
        if (n <= 1)
        {
            return 1;
        }

        if (k == 0 || k == n)
        {
            return 1;
        }

        return Binom(n - 1, k) + Binom(n - 1, k - 1);
    }
}
