namespace _01._Move_Down_Right;

using System;
using System.Numerics;

public static class Program
{
    private static int rows;

    private static int cols;

    private static BigInteger[][] matrix = Array.Empty<BigInteger[]>();

    public static void Main()
    {
        ReadInput();

        CalculatePaths();

        PrintOutput();
    }

    private static void ReadInput()
    {
        rows = int.Parse(Console.ReadLine() ?? "0");

        cols = int.Parse(Console.ReadLine() ?? "0");

        matrix = new BigInteger[rows][];

        for (var i = 0; i < rows; i++)
        {
            matrix[i] = new BigInteger[cols];
        }

        matrix[0][0] = 0;

        for (var i = 0; i < rows; i++)
        {
            matrix[i][0] = 1;
        }

        for (var i = 1; i < cols; i++)
        {
            matrix[0][i] = 1;
        }
    }

    private static void CalculatePaths()
    {
        for (var row = 1; row < rows; row++)
        {
            for (var col = 1; col < cols; col++)
            {
                var left = matrix[row][col - 1];
                var up = matrix[row - 1][col];

                matrix[row][col] = left + up;
            }
        }
    }

    private static void PrintOutput()
        => Console.WriteLine(matrix[rows - 1][cols - 1]);
}
