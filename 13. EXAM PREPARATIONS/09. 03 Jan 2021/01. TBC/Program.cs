namespace _01._TBC;

using System;
using System.Collections.Generic;

public static class Program
{
    private static int rows;

    private static int cols;

    private static char[][] matrix = Array.Empty<char[]>();

    private static bool[][] visited = Array.Empty<bool[]>();

    private static readonly Queue<(int Row, int Col)> queue = new Queue<(int Row, int Col)> ();

    public static void Main()
    {
        ReadInput();

        var count = CountTunnels();

        Console.WriteLine(count);
    }

    private static void ReadInput()
    {
        rows = int.Parse(Console.ReadLine() ?? "0");
        cols = int.Parse(Console.ReadLine() ?? "0");

        matrix = new char[rows][];
        visited = new bool[rows][];

        for (var i = 0; i < rows; i++)
        {
            matrix[i] = Console.ReadLine()?.ToCharArray() ?? Array.Empty<char>();
            visited[i] = new bool[cols];
        }
    }

    private static int CountTunnels()
    {
        var count = 0;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (visited[row][col])
                {
                    continue;
                }

                if (matrix[row][col] == 't')
                {
                    MapTunnel(row, col);
                    count++;
                }
                else
                {
                    visited[row][col] = true;
                }
            }
        }

        return count;
    }

    private static void MapTunnel(int row, int col)
    {
        queue.Enqueue((row, col));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var (currentRow, currentCol) = current;

            visited[currentRow][currentCol] = true;
            AddNeighbours(current);
        }
    }

    private static void AddNeighbours((int Row, int Col) current)
    {
        var (row, col) = current;

        TryAdd(row + 1, col);
        TryAdd(row - 1, col);
        TryAdd(row, col + 1);
        TryAdd(row, col - 1);

        TryAdd(row + 1, col + 1);
        TryAdd(row + 1, col - 1);
        TryAdd(row - 1, col + 1);
        TryAdd(row - 1, col - 1);
    }

    private static void TryAdd(int row, int col)
    {
        if (
            row < 0
            || col < 0
            || row >= rows
            || col >= cols
            || visited[row][col])
        {
            return;
        }

        if (matrix[row][col] == 't')
        {
            queue.Enqueue((row, col));
        }
        else
        {
            visited[row][col] = true;
        }
    }
}
