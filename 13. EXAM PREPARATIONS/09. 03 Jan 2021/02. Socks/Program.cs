namespace _02._Socks;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int[] sequence1;

    private static int[] sequence2;

    private static int[][] matrix;

    private static readonly List<int> lcs;

    static Program()
    {
        sequence1 = Array.Empty<int>();
        sequence2 = Array.Empty<int>();
        matrix = Array.Empty<int[]>();

        lcs = new List<int>();
    }

    public static void Main()
    {
        ReadInput();

        CalculateSequences();

        GenerateLongestCommonSequence();

        Console.WriteLine(lcs.Count);

        // lcs.Reverse();
        // Console.WriteLine(string.Join(" ", lcs));
    }

    private static void ReadInput()
    {
        sequence1 = ReadSequenceFromConsole();

        sequence2 = ReadSequenceFromConsole();
    }

    private static int[] ReadSequenceFromConsole()
        => (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

    private static void CalculateSequences()
    {
        var rows = sequence1.Length;
        var cols = sequence2.Length;

        matrix = new int[rows + 1][];

        for (var i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new int[cols + 1];
        }

        for (var row = 1; row <= rows; row++)
        {
            for (var col = 1; col <= cols; col++)
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
