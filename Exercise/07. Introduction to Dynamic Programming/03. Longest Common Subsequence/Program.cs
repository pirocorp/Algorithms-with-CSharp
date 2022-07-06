namespace _03._Longest_Common_Subsequence;

using System;
using System.Collections.Generic;
using System.Threading.Channels;

public static class Program
{
    private static string sequence1;

    private static string sequence2;

    private static int[][] matrix;

    private static readonly List<char> lcs;

    static Program()
    {
        sequence1 = string.Empty;
        sequence2 = string.Empty;

        matrix = Array.Empty<int[]>();
        lcs = new List<char>();
    }

    public static void Main()
    {
        ReadInput();

        CalculateSequences();

        GenerateLongestCommonSequence();

        PrintOutput();
    }

    private static void ReadInput()
    {
        sequence1 = Console.ReadLine() ?? string.Empty;

        sequence2 = Console.ReadLine() ?? string.Empty;

        matrix = new int[sequence1.Length + 1][];

        for (var i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new int[sequence2.Length + 1];
        }
    }

    private static void PrintOutput()
        => Console.WriteLine(lcs.Count);

    private static void CalculateSequences()
    {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        for (var row = 1; row < rows; row++)
        {
            for (var col = 1; col < cols; col++)
            {
                if (sequence1[row - 1] == sequence2[col - 1])
                {
                    matrix[row][col] = matrix[row - 1][col - 1] + 1;
                }
                else
                {
                    matrix[row][col] = Math.Max(matrix[row][col - 1], matrix[row - 1][col]);
                }
            }
        }
    }

    private static void GenerateLongestCommonSequence()
    {
        var row = matrix.Length - 1;
        var col = matrix[0].Length - 1;

        while (row > 0 && col > 0)
        {
            if (sequence1[row - 1] == sequence2[col - 1]
                && matrix[row][col] == matrix[row - 1][col - 1] + 1)
            {
                lcs.Add(sequence1[row - 1]);

                row -= 1;
                col -= 1;
            }
            else if (matrix[row - 1][col] > matrix[row][col - 1])
            {
                row -= 1;
            }
            else
            {
                col -= 1;
            }
        }
    }
}
