namespace _02._Minimum_Edit_Distance
{
    using System;
    using System.Collections.Generic;

    public static class MinimumEditDistanceProgram
    {
        private static int[,] _costs;
        private static string _s1;
        private static string _s2;

        private static int _costDelete;
        private static int _costInsert;
        private static int _costReplace;

        public static void Main()
        {
            _costReplace = ReadCostFromConsole();
            _costInsert = ReadCostFromConsole();
            _costDelete = ReadCostFromConsole();
            _s1 = ReadStringFromConsole();
            _s2 = ReadStringFromConsole();

            InitializeCosts(_s1.Length, _s2.Length);
            CalculateCosts();
            //PrintCostsMatrix();
            PrintResult();
        }

        private static string ReadStringFromConsole()
        {
            return Console.ReadLine()
                .Split(new []{" = "}, StringSplitOptions.RemoveEmptyEntries)
                [1];
        }

        private static int ReadCostFromConsole()
        {
            return int.Parse(Console.ReadLine()
                .Split(new[] {" = "}, StringSplitOptions.RemoveEmptyEntries)
                [1]);
        }

        private static void CalculateCosts()
        {
            var firstLen = _s1.Length;
            var secondLen = _s2.Length;

            for (var row = 1; row <= firstLen; row++)
            {
                for (var col = 1; col <= secondLen; col++)
                {
                    var delete = _costs[row - 1, col] + _costDelete;
                    var replace = _costs[row - 1, col - 1] + GetCostReplace(row -1, col - 1);
                    var insert = _costs[row, col - 1] + _costInsert;

                    _costs[row, col] = Math.Min(Math.Min(delete, insert), replace);
                }
            }
        }
        
        private static void PrintResult()
        {
            var firstLen = _s1.Length;
            var secondLen = _s2.Length;
            Console.WriteLine("Minimum edit distance: " + _costs[firstLen, secondLen]);
            
            var resultOperations = new Stack<string>();
            var fIndex = firstLen;
            var sIndex = secondLen;
            while (fIndex > 0 && sIndex > 0)
            {
                if (_s1[fIndex - 1] == _s2[sIndex - 1])
                {
                    fIndex--;
                    sIndex--;
                }
                else
                {
                    var replace = _costs[fIndex - 1, sIndex - 1] + _costReplace;
                    var delete = _costs[fIndex - 1, sIndex] + _costDelete;
                    var insert = _costs[fIndex, sIndex - 1] + _costInsert;
                    if (replace <= delete && replace <= insert)
                    {
                        resultOperations.Push($"REPLACE({fIndex - 1}, {_s2[sIndex - 1]})");
                        fIndex--;
                        sIndex--;
                    }
                    else if (delete < replace && delete < insert)
                    {
                        resultOperations.Push($"DELETE({fIndex - 1})");
                        fIndex--;
                    }
                    else
                    {
                        resultOperations.Push($"INSERT({sIndex - 1}, {_s2[sIndex - 1]})");
                        sIndex--;
                    }
                }
            }

            while (fIndex > 0)
            {
                resultOperations.Push($"DELETE({fIndex - 1})");
                fIndex--;
            }

            while (sIndex > 0)
            {
                resultOperations.Push($"INSERT({sIndex - 1}, {_s2[sIndex - 1]})");
                sIndex--;
            }

            foreach (var resultOperation in resultOperations)
            {
                Console.WriteLine(resultOperation);
            }
        }

        private static void InitializeCosts(int rowsCount, int colsCount)
        {
            _costs = new int[rowsCount + 1, colsCount + 1];

            for (var i = 0; i <= rowsCount; i++)
            {
                _costs[i, 0] = i * _costDelete;
            }

            for (var i = 0; i <= colsCount; i++)
            {
                _costs[0, i] = i * _costInsert;
            }
        }
        
        private static int GetCostReplace(int row, int col)
        {
            if (_s1[row] == _s2[col])
            {
                return 0;
            }

            return _costReplace;
        }

        private static void PrintCostsMatrix()
        {
            for (var row = 0; row < _costs.GetLength(0); row++)
            {
                for (var col = 0; col < _costs.GetLength(1); col++)
                {
                    var current = _costs[row, col].ToString().PadLeft(4);
                    Console.Write(current);
                }

                Console.WriteLine();
            }
        }
    }
}
