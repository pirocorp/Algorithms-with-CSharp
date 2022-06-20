namespace _03._Generating_01_Vectors;

using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");
        VectorGenerator(0, Enumerable.Repeat(0, n).ToArray());
    }

    private static void VectorGenerator(int index, int[] vector)
    {
        if (index >= vector.Length)
        {
            Console.WriteLine(string.Join(string.Empty, vector));
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
