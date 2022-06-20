namespace _06._8_Queens_Puzzle;

using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    private const int Size = 8;
    private static int counter = 0;

    private static readonly bool[][] Chessboard = new bool[Size][];

    private static HashSet<int> attackedRows = new HashSet<int>();
    private static HashSet<int> attackedColumns = new HashSet<int>();
    private static HashSet<int> leftDiagonals = new HashSet<int>();
    private static HashSet<int> rightDiagonals = new HashSet<int>();

    public static void Main(string[] args)
    {
        for (var i = 0; i < Chessboard.Length; i++)
        {
            Chessboard[i] = new bool[Size];
        }

        PlaceQueens(0, 0);
        //Console.WriteLine(counter);
    }

    private static void PlaceQueens(int row, int col)
    {
        if (!IsInBounds(row, col))
        {
            return;
        }

        if (PositionIsFree(row, col))
        {
            Mark(row, col);

            if (SolutionIsFound())
            {
                PrintSolution();
                Console.WriteLine();
                counter++;
            }
            else
            {
                PlaceQueens(row + 1, 0);
            }

            
            UnMark(row, col);
        }

        PlaceQueens(row, col + 1);
    }

    private static bool IsInBounds(int row, int col)
        => row < Size && col < Size && row >= 0 && col >= 0;

    private static bool PositionIsFree(int row, int col)
        => !attackedRows.Contains(row) 
           && !attackedColumns.Contains(col)
           && !leftDiagonals.Contains(col - row)
           && !rightDiagonals.Contains(col + row);

    private static bool SolutionIsFound()
        => Chessboard.Sum(r => r.Sum(x => x ? 1 : 0)) == 8;

    private static void Mark(int row, int col)
    {
        Chessboard[row][col] = true;

        attackedRows.Add(row);
        attackedColumns.Add(col);

        var leftDiag = col - row;
        var rightDiag = col + row;

        leftDiagonals.Add(leftDiag);
        rightDiagonals.Add(rightDiag);
    }

    private static void UnMark(int row, int col)
    {
        Chessboard[row][col] = false;

        attackedRows.Remove(row);
        attackedColumns.Remove(col);

        var leftDiag = col - row;
        var rightDiag = col + row;

        leftDiagonals.Remove(leftDiag);
        rightDiagonals.Remove(rightDiag);
    }

    private static void PrintSolution()
        => Chessboard
            .Select(r => r.Select(e => e ? "*" : "-"))
            .ToList()
            .ForEach(r => Console.WriteLine(string.Join(" ", r)));
}
