namespace _02._Areas_in_Matrix;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static string[] matrix;
    private static bool[][] visited;

    private static readonly Dictionary<char, int> areas;

    static Program()
    {
        matrix = Array.Empty<string>();
        visited = Array.Empty<bool[]>();

        areas = new Dictionary<char, int>();
    }

    public static void Main()
    {
        ReadInput();

        MapAreas();

        PrintOutput();
    }

    private static void ReadInput()
    {
        var rows = int.Parse(Console.ReadLine() ?? "0");
        var cols = int.Parse(Console.ReadLine() ?? "0");

        matrix = new string[rows];
        visited = new bool[rows][];

        for (var i = 0; i < rows; i++)
        {
            visited[i] = new bool[cols];

            matrix[i] = Console.ReadLine() ?? string.Empty;
        }
    }

    private static void MapAreas()
    {
        for (var row = 0; row < matrix.Length; row++)
        {
            var matrixRow = matrix[row];

            for (var col = 0; col < matrixRow.Length; col++)
            {
                if (visited[row][col])
                {
                    continue;
                }
                
                MapArea(row, col);
            }
        }
    }

    private static void MapArea(int row, int col)
    {
        var target = matrix[row][col];

        MapCells(row, col, target);

        if (!areas.ContainsKey(target))
        {
            areas[target] = 0;
        }

        areas[target]++;
    }

    private static void MapCells(int row, int col, char target)
    {
        visited[row][col] = true;

        var up = row - 1;

        if (up >= 0 && matrix[up][col] == target && !visited[up][col])
        {
            MapCells(up, col, target);
        }

        var down = row + 1;

        if (down < matrix.Length && matrix[down][col] == target && !visited[down][col])
        {
            MapCells(down, col, target);
        }

        var left = col - 1;

        if (left >= 0 && matrix[row][left] == target && !visited[row][left])
        {
            MapCells(row, left, target);
        }

        var right = col + 1;

        if (right < matrix[row].Length && matrix[row][right] == target && !visited[row][right])
        {
            MapCells(row, right, target);
        }
    }

    private static void PrintOutput()
    {
        Console.WriteLine($"Areas: {areas.Sum(a => a.Value)}");

        areas
            .OrderBy(a => a.Key)
            .ToList()
            .ForEach(a => Console.WriteLine($"Letter '{a.Key}' -> {a.Value}"));
    }
}
