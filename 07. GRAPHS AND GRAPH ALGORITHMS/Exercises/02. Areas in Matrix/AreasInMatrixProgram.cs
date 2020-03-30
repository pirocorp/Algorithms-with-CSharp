namespace _02._Areas_in_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AreasInMatrixProgram
    {
        private static char[][] _matrix;
        private static bool[][] _visited;

        private static Dictionary<char, int> _areas;

        public static void Main()
        {
            ReadInput();
            MapAreas();

            Console.WriteLine($"Areas: {_areas.Sum(x => x.Value)}");

            foreach (var key in _areas.Keys.OrderBy(x => x))
            {
                Console.WriteLine($"Letter '{key}' -> {_areas[key]}");
            }
        }

        private static void MapAreas()
        {
            _areas = new Dictionary<char, int>();

            for (var rowIndex = 0; rowIndex < _matrix.Length; rowIndex++)
            {
                var currentRow = _matrix[rowIndex];

                for (var colIndex = 0; colIndex < currentRow.Length; colIndex++)
                {
                    if (_visited[rowIndex][colIndex])
                    {
                        continue;
                    }

                    var currentChar = _matrix[rowIndex][colIndex];

                    if (!_areas.ContainsKey(currentChar))
                    {
                        _areas.Add(currentChar, 0);
                    }

                    _areas[currentChar]++;
                    MapArea(rowIndex, colIndex);
                }
            }
        }

        private static void MapArea(int rowIndex, int colIndex)
        {
            var areaName = _matrix[rowIndex][colIndex];

            var elements = new Queue<Tuple<int, int>>();
            elements.Enqueue(new Tuple<int, int>(rowIndex, colIndex));

            //BFS
            while (elements.Count > 0)
            {
                var (row, col) = elements.Dequeue();
                _visited[row][col] = true;

                //Go Right
                if (col + 1 < _matrix[row].Length 
                    && _matrix[row][col + 1] == areaName
                    && !_visited[row][col + 1])
                {
                    elements.Enqueue(new Tuple<int, int>(row, col + 1));
                }

                //Go Down
                if (row + 1 < _matrix.Length
                    && _matrix[row + 1][col] == areaName
                    && !_visited[row + 1][col])
                {
                    elements.Enqueue(new Tuple<int, int>(row + 1, col));
                }

                //Go Left
                if (col - 1 >= 0 
                    && _matrix[row][col - 1] == areaName
                    && !_visited[row][col - 1])
                {
                    elements.Enqueue(new Tuple<int, int>(row, col - 1));
                }

                //Go Up
                if (row - 1 >= 0
                    && _matrix[row - 1][col] == areaName
                    && !_visited[row - 1][col])
                {
                    elements.Enqueue(new Tuple<int, int>(row - 1, col));
                }
            }
        }

        private static void ReadInput()
        {
            var totalRows = int.Parse(Console.ReadLine());
            _matrix = new char[totalRows][];
            _visited = new bool[totalRows][];

            for (var i = 0; i < totalRows; i++)
            {
                _matrix[i] = Console.ReadLine().ToCharArray();
                _visited[i] = new bool[_matrix[i].Length];
            }
        }
    }
}
