namespace _05._Word_Differences;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var sequence1 = Console.ReadLine() ?? string.Empty;
        var sequence2 = Console.ReadLine() ?? string.Empty;

        var lcs = new LcsCalculator().Calculate(sequence1, sequence2);

        var min = Math.Min(sequence1.Length, sequence2.Length);
        var max = Math.Max(sequence1.Length, sequence2.Length);

        var count = ((min - lcs.Count)* 2) + (max - min);

        Console.WriteLine($"Deletions and Insertions: {count}");
    }
}

internal class LcsCalculator
{
    private string sequence1;

    private string sequence2;

    private int[][] matrix;

    private readonly List<char> lcs;

    public LcsCalculator()
    {
        this.sequence1 = string.Empty;
        this.sequence2 = string.Empty;

        this.matrix = Array.Empty<int[]>();
        this.lcs = new List<char>();
    }

    public List<char> Calculate(string str1, string str2)
    {
        this.Initialize(str1, str2);

        this.CalculateSequences();

        this.GenerateLongestCommonSequence();

        return this.lcs.ToList();
    }

    private void Initialize(string str1, string str2)
    {
        this.sequence1 = str1;

        this.sequence2 = str2;

        this.matrix = new int[this.sequence1.Length + 1][];

        for (var i = 0; i < this.matrix.Length; i++)
        {
            this.matrix[i] = new int[this.sequence2.Length + 1];
        }
    }

    private void CalculateSequences()
    {
        var rows = this.matrix.Length;
        var cols = this.matrix[0].Length;

        for (var row = 1; row < rows; row++)
        {
            for (var col = 1; col < cols; col++)
            {
                if (this.sequence1[row - 1] == this.sequence2[col - 1])
                {
                    this.matrix[row][col] = this.matrix[row - 1][col - 1] + 1;
                }
                else
                {
                    this.matrix[row][col] = Math.Max(this.matrix[row][col - 1], this.matrix[row - 1][col]);
                }
            }
        }
    }

    private void GenerateLongestCommonSequence()
    {
        var row = this.matrix.Length - 1;
        var col = this.matrix[0].Length - 1;

        while (row > 0 && col > 0)
        {
            if (this.sequence1[row - 1] == this.sequence2[col - 1]
                && this.matrix[row][col] == this.matrix[row - 1][col - 1] + 1)
            {
                this.lcs.Add(this.sequence1[row - 1]);

                row -= 1;
                col -= 1;
            }
            else if (this.matrix[row - 1][col] > this.matrix[row][col - 1])
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
