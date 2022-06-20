namespace _00._Demo;

using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        // SumArray();

        // Console.WriteLine(GetFactorial(int.Parse(Console.ReadLine() ?? "0")));
        // PrintFigure(5);

        // VectorGenerator(0, Enumerable.Repeat(0, 5).ToArray());
    }

    private static void SumArray()
    {
        var input = Console.ReadLine()
                        ?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x.Trim()))
                        .ToArray()
                    ?? Array.Empty<int>();

        var result = Sum(input, 0);

        Console.WriteLine(result);
    }

    private static int Sum(int[] array, int index)
    {
        if (array.Length == 0)
        {
            return 0;
        }

        if (index == array.Length - 1)
        {
            return array[index];
        }

        return array[index] + Sum(array, index + 1);
    }

    private static long GetFactorial(int num)
    {
        if (num == 0)
        {
            return 1;
        } 
   
        return num * GetFactorial(num - 1);
    } 

    private static void PrintFigure(int n)
    {
        if (n == 0)
            return;

        // TODO: Pre-action: print n asterisks
        Console.WriteLine(new string('*', n));
        PrintFigure(n - 1);
        // TODO: Post-action: print n hashtags
        Console.WriteLine(new string('#', n));
    }

    private static void VectorGenerator(int index, int[] vector)
    {
        if (index >= vector.Length)
        {
            Console.WriteLine(string.Join(" ", vector));
        }
        else
        {
            for (var i = 0; i <= 1; i++)
            {
                vector[index] = i;
                VectorGenerator(index + 1, vector);
            }
        }
    }
}
