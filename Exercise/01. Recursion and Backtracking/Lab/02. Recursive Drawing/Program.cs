namespace _02._Recursive_Drawing;

using System;

public class Program
{
    public static void Main()
    {
        PrintFigure(int.Parse(Console.ReadLine() ?? "0"));
    }

    private static void PrintFigure(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(new string('*', n));

        PrintFigure(n - 1);

        Console.WriteLine(new string('#', n));
    }
}
