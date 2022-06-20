namespace _05._Paths_in_Labyrinth;

using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static readonly List<char> Path = new List<char>();

    private static int rows;
    private static int cols;
    private static List<string> labyrinth = new List<string>();
    private static bool[,] visited = new bool[0, 0];

    public static void Main(string[] args)
    {
        labyrinth = ReadLabyrinth();
        FindPaths(0, 0, 'S');
    }

    private static void FindPaths(int row, int col, char direction)
    {
        if (!IsInBounds(row, col))
        {
            return;
        }

        Path.Add(direction);

        if (IsExit(row, col))
        {
            PrintPath();
        }
        else if (!IsVisited(row, col) && IsFree(row, col))
        {
            Mark(row, col);
            FindPaths(row, col + 1, 'R');
            FindPaths(row + 1, col, 'D');
            FindPaths(row, col - 1, 'L');
            FindPaths(row - 1, col, 'U');
            UnMark(row, col);
        }

        Path.RemoveAt(Path.Count - 1);
    }

    private static void Mark(int row, int col)
        => visited[row, col] = true;

    private static void UnMark(int row, int col)
        => visited[row, col] = false;

    private static bool IsExit(int row, int col)
        => labyrinth[row][col] == 'e';

    private static bool IsInBounds(int row, int col)
        => row < rows && col < cols && row >= 0 && col >= 0;

    private static bool IsVisited(int row, int col)
        => visited[row, col];

    private static bool IsFree(int row, int col)
        => labyrinth[row][col] == '-';

    private static List<string> ReadLabyrinth()
    {
        labyrinth = new List<string>();
        
        rows = int.Parse(Console.ReadLine() ?? "0");
        cols = int.Parse(Console.ReadLine() ?? "0");

        visited = new bool[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            labyrinth.Add(Console.ReadLine() ?? string.Empty);
        }

        return labyrinth;
    }

    private static void PrintPath()
        => Console.WriteLine(string.Join("", Path.Skip(1)));
}
