namespace _02._Move_Down_Right;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int rows;

    private static int cols;

    private static int[][] matrix;

    private static long[][] solutions;

    private static readonly Stack<(int Row, int Col)> solution;

    static Program()
    {
        matrix = Array.Empty<int[]>();
        solutions = Array.Empty<long[]>();
        solution = new Stack<(int Row, int Col)>();
    }

    public static void Main()
    {
        ReadInput();

        CalculateSolutions();

        GenerateSolution();

        PrintOutput();
    }

    private static void ReadInput()
    {
        rows = int.Parse(Console.ReadLine() ?? "0");
        cols = int.Parse(Console.ReadLine() ?? "0");

        matrix = new int[rows][];

        for (var i = 0; i < rows; i++)
        {
            matrix[i] = (Console.ReadLine() ?? string.Empty)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(int.Parse)
                .ToArray();
        }

        InitializeSolutionMatrix();
    }

    private static void PrintOutput()
        => Console.WriteLine(string.Join(" ", solution.ToArray().Select(x => $"[{x.Row}, {x.Col}]")));

    private static void InitializeSolutionMatrix()
    {
        solutions = new long[rows][];

        for (var i = 0; i < solutions.Length; i++)
        {
            solutions[i] = new long[cols];

            if (i == 0)
            {
                solutions[0][0] = matrix[0][0];

                for (var j = 1; j < matrix[0].Length; j++)
                {
                    solutions[0][j] = matrix[0][j] + solutions[0][j - 1];
                }
            }
            else
            {
                solutions[i][0] = matrix[i][0] + solutions[i - 1][0];
            }
        }
    }

    private static void CalculateSolutions()
    {
        for (var row = 1; row < rows; row++)
        {
            for (var col = 1; col < cols; col++)
            {
                var max = Math.Max(solutions[row][col - 1], solutions[row - 1][col]);

                solutions[row][col] = max + matrix[row][col];
            }
        }
    }

    private static void GenerateSolution()
    {
        var currentRow = rows - 1;
        var currentCol = cols - 1;

        while (currentRow >= 0 && currentCol >= 0)
        {
            solution.Push((currentRow, currentCol));

            if (currentCol == 0)
            {
                currentRow--;
                continue;
            }

            if (currentRow == 0)
            {
                currentCol--;
                continue;
            }


            var leftSum = solutions[currentRow][currentCol - 1];
            var upSum = solutions[currentRow - 1][currentCol];

            if (upSum > leftSum)
            {
                currentRow--;
            }
            else
            {
                currentCol--;
            }
        }
    }
}
