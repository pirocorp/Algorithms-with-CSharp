namespace _03._Connected_Areas_in_Matrix;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static int rows;

    private static int cols;

    private static string[] matrix = Array.Empty<string>();

    private static bool[][] visited = Array.Empty<bool[]>();

    private static int size = 0;

    private static readonly SortedSet<Area> areas = new SortedSet<Area>();

    public static void Main()
    {
        ReadInput();

        while (FindTraversableCell(out var cell))
        {
            TraverseArea(cell);

            MarkTraversedArea(cell);
        }

        PrintResult();
    }

    private static void ReadInput()
    {
        rows = int.Parse(Console.ReadLine() ?? "0");
        cols = int.Parse(Console.ReadLine() ?? "0");

        matrix = new string[rows];
        visited = new bool[rows][];

        for (var i = 0; i < rows; i++)
        {
            matrix[i] = Console.ReadLine() ?? string.Empty;
            visited[i] = new bool[cols];
        }
    }

    private static void PrintResult()
    {
        Console.WriteLine($"Total areas found: {areas.Count}");

        var result = areas.ToList();

        for (var i = 0; i < result.Count; i++)
        {
            var area = result[i];
            Console.WriteLine($"Area #{i + 1} at ({area.Row}, {area.Col}), size: {area.Size}");
        }
    }

    private static bool FindTraversableCell(out (int row, int col) cell)
    {
        const char nonTraversableCharacter = '*';

        cell = (-1, -1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i][j] == nonTraversableCharacter || visited[i][j])
                {
                    continue;
                }

                cell = (i, j);
                return true;
            }
        }

        return false;
    }

    private static void TraverseArea((int row, int col) cell)
    {
        TraverseCell(cell);

        var (row, col) = cell;

        if (row > 0 && TraversableCell(row - 1, col))
        {
            TraverseArea((row - 1, col));
        }

        if (row < rows - 1 && TraversableCell(row + 1, col))
        {
            TraverseArea((row + 1, col));
        }

        if (col > 0 && TraversableCell(row, col - 1))
        {
            TraverseArea((row, col - 1));
        }

        if (col < cols - 1 && TraversableCell(row, col + 1))
        {
            TraverseArea((row, col + 1));
        }
    }

    private static void TraverseCell((int row, int col) cell)
    {
        var (row, col) = cell;

        visited[row][col] = true;
        size++;
    }

    private static bool TraversableCell(int row, int col)
        => !visited[row][col] && matrix[row][col] != '*';

    private static void MarkTraversedArea((int row, int col) cell)
    {
        var (row, col) = cell;
        var area = new Area(row, col, size);

        areas.Add(area);
        size = 0;
    }
}

internal class Area : IComparable<Area>
{
    public Area(int row, int col, int size)
    {
        this.Row = row;
        this.Col = col;
        this.Size = size;
    }

    public int Row { get; }

    public int Col { get; }

    public int Size { get; }

    public int CompareTo(Area? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Cannot compare Area type to null");
        }


        if (other.Size - this.Size != 0)
        {
            return other.Size - this.Size;
        }
        else if (this.Row - other.Row != 0)
        {
            return this.Row - other.Row;
        }
        else
        {
            return this.Col - other.Col;
        }
    }
}
