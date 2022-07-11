namespace _06._Connecting_Cables;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var sequence2 = (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToList();

        var n = sequence2.Count;
        var sequence1 = Enumerable.Range(1, n).ToList();

        var lcs = new LcsCalculator<int>().Calculate(sequence1, sequence2);

        Console.WriteLine($"Maximum pairs connected: {lcs.Count}");
    }
}

internal class LcsCalculator<T> where T : notnull
{
    private List<T> sequence1;

    private List<T> sequence2;

    private int[][] matrix;

    private readonly List<T> lcs;

    public LcsCalculator()
    {
        this.sequence1 = new List<T>();
        this.sequence2 = new List<T>();

        this.matrix = Array.Empty<int[]>();
        this.lcs = new List<T>();
    }

    public List<T> Calculate(List<T> str1, List<T> str2)
    {
        this.Initialize(str1, str2);

        this.CalculateSequences();

        this.GenerateLongestCommonSequence();

        return this.lcs.ToList();
    }

    private void Initialize(List<T> str1, List<T> str2)
    {
        this.sequence1 = str1;

        this.sequence2 = str2;

        this.matrix = new int[this.sequence1.Count + 1][];

        for (var i = 0; i < this.matrix.Length; i++)
        {
            this.matrix[i] = new int[this.sequence2.Count + 1];
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
                if (this.sequence1[row - 1].Equals(this.sequence2[col - 1]))
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
            if (this.sequence1[row - 1].Equals(this.sequence2[col - 1])
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
