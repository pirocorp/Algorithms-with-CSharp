﻿namespace _00._Conceptions
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    public static class ConceptionsProgram
    {
        public static void Main()
        {
            //ReduceToBasicOperation();
            //FromRecursionToDynamicPrograming();
            //LongestCommonSubsequence();

            //02:37:24 continue
        }

        private static void LongestCommonSubsequence()
        {
            var first = "ABCBDAB";
            var second = "BDCABA";

            var lcs = new int[first.Length + 1, second.Length + 1];
            CalculateLcs(first, second, lcs);

            //PrintMatrix(lcs);
            //Console.WriteLine(lcs[first.Length, second.Length]);
            var result = ReconstructSolution(first, second, lcs);
            Console.WriteLine(result);
            var result2 = ReconstructSolutions(first, second, lcs);
            Console.WriteLine();
            Console.WriteLine(string.Join(Environment.NewLine, result2));
        }

        private static void CalculateLcs(string first, string second, int[,] lcs)
        {
            for (var row = 1; row <= first.Length; row++)
            {
                for (var col = 1; col <= second.Length; col++)
                {
                    if (first[row - 1] == second[col - 1])
                    {
                        lcs[row, col] = lcs[row - 1, col - 1] + 1;
                    }
                    else
                    {
                        lcs[row, col] = Math.Max(lcs[row - 1, col], lcs[row, col - 1]);
                    }
                }
            }
        }

        //Backtracking
        private static string ReconstructSolution(string first, string second, int[,] lcs)
        {
            var subsequence = new StringBuilder();

            var row = first.Length;
            var col = second.Length;

            while (row > 0 && col > 0)
            {
                if (first[row - 1] == second[col - 1])
                {
                    subsequence.Insert(0, first[row - 1]);

                    --row;
                    --col;
                }
                else if (lcs[row - 1, col] > lcs[row, col - 1])
                {
                    --row;
                }
                else
                {
                    --col;
                }
            }

            return subsequence.ToString();
        }

        private static List<string> ReconstructSolutions(string first, string second, int[,] lcs)
        {
            var result = new List<string>();
            var subsequence = new StringBuilder();
            ReconstructSolutions(first, second, lcs, first.Length, second.Length, subsequence, result);
            return result;
        }

        private static void ReconstructSolutions(
            string first, string second, int[,] lcs, int row, int col, StringBuilder subsequence, List<string> result)
        {
            if (row == 0 || col == 0)
            {
                result.Add(subsequence.ToString());
                return;
            }

            if (first[row - 1] == second[col - 1])
            {
                subsequence.Insert(0, first[row - 1]);
                ReconstructSolutions(first, second, lcs, row - 1, col - 1, subsequence, result);
                subsequence.Remove(0, 1);
                --row;
                --col;
            }
            else if (lcs[row - 1, col] > lcs[row, col - 1])
            {
                ReconstructSolutions(first, second, lcs, row - 1, col, subsequence, result);
            }
            else if (lcs[row - 1, col] < lcs[row, col - 1])
            {
                ReconstructSolutions(first, second, lcs, row, col - 1, subsequence, result);
            }
            else
            {
                ReconstructSolutions(first, second, lcs, row - 1, col, subsequence, result);
                ReconstructSolutions(first, second, lcs, row, col - 1, subsequence, result);
            }
        }

        private static void ReduceToBasicOperation()
        {
            var count = 0;

            var arr = new int[] { 1, 6, 45, 2, 7, 0, 8, 5, 9, -3, 7 };
            var result = FindMinimalRecursive(arr, arr.Length, ref count);
            var result2 = FindMinimalBottomUp(arr);
            Console.WriteLine(result);
            Console.WriteLine(result2);
            //Console.WriteLine(count);
        }

        private static int FindMinimalBottomUp(int[] array)
        {
            var minimums = new int[array.Length];
            minimums[0] = array[0];

            for (var i = 1; i < array.Length; i++)
            {
                minimums[i] = Math.Min(minimums[i - 1], array[i]);
            }

            //^1 index from end to start
            return minimums[^1];
        }

        private static int FindMinimalRecursive(int[] array, int length, ref int count)
        {
            count++;

            if (length == 1)
            {
                return array[0];
            }

            var minTillNow = FindMinimalRecursive(array, length - 1, ref count);

            return Math.Min(minTillNow, array[length - 1]);
        }

        private static void FromRecursionToDynamicPrograming()
        {
            var map = new string[]
            {
                "                    ",
                "                    ",
                "                    ",
                "          @         ",
                "                    ",
                "                    ",
                "                    ",
                "      @             ",
                "             @      ",
                "                    ",
                "                    ",
                "          @         ",
                "                    ",
                "      @             ",
                "                    ",
                "                    ",
                "                    ",
                "                    ",
                "                    ",
                "                  @ ",
            };

            var memo = new BigInteger[map.Length, map[0].Length];

            for (var row = 0; row < memo.GetLength(0); row++)
            {
                for (var col = 0; col < memo.GetLength(1); col++)
                {
                    memo[row, col] = -1;
                }
            }

            var result = DfsRecursion(map, memo, 0, 0);
            Console.WriteLine(result);

            var result2 = DpBottomUp(map);
            Console.WriteLine(result2);

            var result3 = DpBottomUpMemoryOptimization(map);
            Console.WriteLine(result3);
        }

        private static BigInteger DfsRecursion(string[] map, BigInteger[,] memo, int row, int col)
        {
            if (row >= map.Length || col >= map[row].Length)
            {
                return 0;
            }

            if (row == map.Length - 1 && col == map[row].Length - 1)
            {
                //Console.WriteLine("Find a way");
                return 1;
            }

            if (map[row][col] != ' ')
            {
                return 0;
            }

            if (memo[row, col] == -1)
            {
                var down = DfsRecursion(map, memo, row + 1, col);
                var right = DfsRecursion(map, memo, row, col + 1);

                memo[row, col] = down + right;
            }

            return memo[row, col];
        }

        private static BigInteger DpBottomUp(string[] map)
        {
            var totalRows = map.Length;
            var totalCols = map[0].Length;

            var dp = new BigInteger[totalRows, totalCols];
            dp[0, 0] = 1;

            for (var row = 0; row < dp.GetLength(0); row++)
            {
                for (var col = 0; col < dp.GetLength(1); col++)
                {
                    if (row == 0 && col == 0)
                    {
                        continue;
                    }

                    var fromUp = row > 0 ? dp[row - 1, col] : 0;
                    var fromLeft = col > 0 ? dp[row, col - 1] : 0;

                    dp[row, col] = map[row][col] == ' '
                        ? fromUp + fromLeft
                        : 0;
                }
            }

            return dp[totalRows - 1, totalCols - 1];
        }

        private static BigInteger DpBottomUpMemoryOptimization(string[] map)
        {
            var totalRows = map.Length;
            var totalCols = map[0].Length;

            var dp = new BigInteger[2, totalCols];
            dp[0, 0] = 1;

            for (var row = 0; row < totalRows; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        continue;
                    }

                    var fromUp = row > 0 ? dp[(row - 1) % 2, col] : 0;
                    var fromLeft = col > 0 ? dp[row % 2, col - 1] : 0;

                    dp[row % 2, col] = map[row][col] == ' '
                        ? fromUp + fromLeft
                        : 0;
                }
            }

            return dp[(totalRows - 1) % 2, totalCols - 1];
        }

        private static void PrintMatrix(int[,] matrix)
        {
            var totalRows = matrix.GetLength(0);
            var totalCols = matrix.GetLength(1);

            for (var row = 0; row < totalRows; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    Console.Write(matrix[row, col].ToString().PadLeft(5));
                }

                Console.WriteLine();
            }
        }
    }
}
