namespace _03._Climbing;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int rows;

    private static int cols;

    private static int[][] matrix;

    private static long[][] solutions;

    private static readonly List<long> solution;

    static Program()
    {
        matrix = Array.Empty<int[]>();
        solutions = Array.Empty<long[]>();
        solution = new List<long>();
    }

    public static void Main()
    {
        ReadInput();

        InitializeSolutionMatrix();

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
    }

    private static void InitializeSolutionMatrix()
    {
        solutions = new long[rows][];

        var lastRow = rows - 1;
        var lastCol = cols - 1;

        for (var row = lastRow; row >= 0; row--)
        {
            solutions[row] = new long[cols];

            if (row == lastRow)
            {
                solutions[lastRow][lastCol] = matrix[lastRow][lastCol];

                for (var col = lastCol - 1; col >= 0; col--)
                {
                    solutions[lastRow][col] = matrix[lastRow][col] + solutions[lastRow][col + 1];
                }
            }
            else
            {
                solutions[row][lastCol] = matrix[row][lastCol] + solutions[row + 1][lastCol];
            }
        }
    }

    private static void CalculateSolutions()
    {
        var lastRow = rows - 1;
        var lastCol = cols - 1;

        for (var row = lastRow - 1; row >= 0; row--)
        {
            for (var col = lastCol - 1; col >= 0; col--)
            {
                var max = Math.Max(solutions[row][col + 1], solutions[row + 1][col]);

                solutions[row][col] = max + matrix[row][col];
            }
        }
    }

    private static void GenerateSolution()
    {
        var currentRow = 0;
        var currentCol = 0;

        var lastRow = rows - 1;
        var lastCol = cols - 1;

        while (currentRow <= lastRow && currentCol <= lastCol)
        {
            solution.Add(matrix[currentRow][currentCol]);

            if (currentCol == lastCol)
            {
                currentRow++;
                continue;
            }

            if (currentRow == lastRow)
            {
                currentCol++;
                continue;
            }

            var rightSum = solutions[currentRow][currentCol + 1];
            var downSum = solutions[currentRow + 1][currentCol];

            if (downSum > rightSum)
            {
                currentRow++;
            }
            else
            {
                currentCol++;
            }
        }
    }

    private static void PrintOutput()
    {
        Console.WriteLine(solutions[0][0]);
        solution.Reverse();
        Console.WriteLine(string.Join(" ", solution));
    }
}
