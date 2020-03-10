namespace _08._Permutation_Based_Solution_8_Queens_Problem
{
    using System;

    public static class PermutationBasedSolutionQueensProblemProgram
    {
        public static void Main()
        {
            var chessBoardSize = int.Parse(Console.ReadLine());
            GetQueens(chessBoardSize);
        }

        private static void GetQueens(int chessBoardSize)
        {
            var queens = new int[chessBoardSize];
            GetQueens(queens, 0);
        }

        private static void GetQueens(int[] queens, int currentQueenIndex)
        {
            var queensCount = queens.Length;
            if (currentQueenIndex == queensCount)
            {
                PrintQueens(queens);
            }
            else
            {
                for (var i = 0; i < queensCount; i++)
                {
                    queens[currentQueenIndex] = i;
                    if (IsConsistent(queens, currentQueenIndex))
                    {
                        GetQueens(queens, currentQueenIndex + 1);
                    }
                }
            }
        }

        private static void PrintQueens(int[] queens)
        {
            var n = queens.Length;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (queens[i] == j)
                    {
                        Console.Write("Q ");
                    }
                    else
                    {
                        Console.Write("* ");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool IsConsistent(int[] queens, int currentQueenIndex)
        {
            for (var i = 0; i < currentQueenIndex; i++)
            {
                if (queens[i] == queens[currentQueenIndex])
                {
                    return false; //same column
                }

                if (queens[i] - queens[currentQueenIndex] == currentQueenIndex - i)
                {
                    return false; //same main diagonal
                }

                if (queens[currentQueenIndex] - queens[i] == currentQueenIndex - i)
                {
                    return false; //same anti diagonal
                }
            }

            return true;
        }
    }
}
