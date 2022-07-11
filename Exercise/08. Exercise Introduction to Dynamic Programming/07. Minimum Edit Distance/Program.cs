namespace _07._Minimum_Edit_Distance;

using System;
using System.Linq;

public static class Program
{
    private static string sequence1;

    private static string sequence2;

    private static int[][] matrix;

    private static int replacement;

    private static int insert;

    private static int delete;

    static Program()
    {
        sequence1 = string.Empty;
        sequence2 = string.Empty;

        matrix = Array.Empty<int[]>();
    }

    public static void Main()
    {
        ReadInput();

        InitializeMatrix();

        CalculateEditDistance();

        PrintOutput();
    }

    private static void ReadInput()
    {
        replacement = int.Parse(Console.ReadLine() ?? "0");

        insert = int.Parse(Console.ReadLine() ?? "0");

        delete = int.Parse(Console.ReadLine() ?? "0");

        sequence1 = Console.ReadLine() ?? string.Empty;

        sequence2 = Console.ReadLine() ?? string.Empty;

        matrix = new int[sequence1.Length + 1][];

        for (var i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new int[sequence2.Length + 1];
        }
    }

    private static void InitializeMatrix()
    {
        matrix[0][0] = 0;

        for (var i = 1; i < matrix.Length; i++)
        {
            matrix[i][0] = matrix[i - 1][0] + delete;
        }

        for (var i = 1; i < matrix[0].Length; i++)
        {
            matrix[0][i] = matrix[0][i - 1] + insert;
        }
    }

    private static void CalculateEditDistance()
    {
        var rows = matrix.Length;

        var cols = matrix[0].Length;

        for (var row = 1; row < rows; row++)
        {
            for (var col = 1; col < cols; col++)
            {
                var left = matrix[row - 1][col] + delete;
                var up = matrix[row][col - 1] + insert;
                var replace = 0;

                if (sequence1[row -1] != sequence2[col - 1])
                {
                    replace += replacement;
                }

                replace += matrix[row - 1][col - 1];

                var min = Math.Min(left, Math.Min(up, replace));
                matrix[row][col] = min;
            }
        }
    }

    private static void PrintOutput() 
        => Console.WriteLine($"Minimum edit distance: {matrix.Last().Last()}");
}
