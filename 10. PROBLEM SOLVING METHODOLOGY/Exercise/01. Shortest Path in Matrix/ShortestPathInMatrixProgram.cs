namespace _01._Shortest_Path_in_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ShortestPathInMatrixProgram
    {
        private static int[][] _matrix;
        private static Dictionary<Cell, List<Cell>> _graph;
        private static Dictionary<Cell, int> _bestPath;
        private static Dictionary<Cell, Cell> _prevCell;
        private static Cell _startCell;

        private static void ReadInput()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            _matrix = new int[rows][];

            for (var row = 0; row < _matrix.Length; row++)
            {
                var currentRow = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                _matrix[row] = currentRow;
            }
        }

        private static void PrintMatrix(int[][] matrix)
        {
            Console.WriteLine();

            for (var row = 0; row < matrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ", matrix[row].Select(x => x.ToString().PadLeft(4)).ToArray()));
            }
        }

        private static bool IsInMatrix(int row, int col, int[][] matrix)
        {
            return row >= 0 && row < matrix.Length 
                && col >= 0 && col < matrix[row].Length;
        }

        private static List<Cell> GetNeighbours(Cell cell)
        {
            var neighbours = new List<Cell>();

            var coordinates = new int[]
            {
                //Up
                cell.Row - 1,
                cell.Col,
                //Right
                cell.Row,
                cell.Col + 1,
                //Down
                cell.Row + 1,
                cell.Col,
                //Left
                cell.Row,
                cell.Col - 1,
            };

            for (var i = 0; i < 8; i += 2)
            {
                var row = coordinates[i];
                var col = coordinates[i + 1];

                if (IsInMatrix(row, col, _matrix))
                {
                    neighbours.Add(new Cell(row, col, _matrix[row][col]));
                }
            }

            return neighbours;
        }

        private static void BuildGraph()
        {
            _graph = new Dictionary<Cell, List<Cell>>();
            _bestPath = new Dictionary<Cell, int>();
            _prevCell = new Dictionary<Cell, Cell>();

            for (var row = 0; row < _matrix.Length; row++)
            {
                for (var col = 0; col < _matrix[row].Length; col++)
                {
                    var cell = new Cell(row, col, _matrix[row][col]);
                    _bestPath.Add(cell, int.MaxValue);

                    if (row == 0 && col == 0)
                    {
                        _startCell = cell;
                    }

                    var neighbours = GetNeighbours(cell);
                    _graph[cell] = neighbours;
                }
            }
        }

        private static Cell Dijkstra()
        {
            var queue = new PriorityQueue<Cell>();
            queue.Enqueue(_startCell);

            while (queue.Count > 0)
            {
                var currentCell = queue.ExtractMin();

                var row = _matrix.Length - 1;
                var col = _matrix[row].Length - 1;

                if (currentCell.Row == row
                    && currentCell.Col == col)
                {
                    return currentCell;
                }

                var neighbours = _graph[currentCell];

                foreach (var neighbour in neighbours)
                {
                    if (currentCell.Value + neighbour.Value < _bestPath[neighbour])
                    {
                        var currentPathValue = currentCell.Value + neighbour.Value;
                        neighbour.Value = currentPathValue;
                        _bestPath[neighbour] = currentPathValue;

                        if (queue.Contains(neighbour))
                        {
                            queue.DecreaseKey(neighbour);
                        }
                        else
                        {
                            queue.Enqueue(neighbour);
                        }

                        _prevCell.Add(neighbour, currentCell);
                    }
                }
            }

            return null;
        }

        private static void PrintResult(Cell destinationCell)
        {
            if (destinationCell == null)
            {
                Console.WriteLine("Path not found!");
                return;
            }

            Console.WriteLine($"Length: {destinationCell.Value}");

            var path = new List<int>();
            path.Add(_matrix[destinationCell.Row][destinationCell.Col]);

            while (_prevCell.ContainsKey(destinationCell))
            {
                destinationCell = _prevCell[destinationCell];
                path.Add(_matrix[destinationCell.Row][destinationCell.Col]);

                if (destinationCell.Row == 0 && destinationCell.Col == 0)
                {
                    break;
                }
            }

            path.Reverse();
            Console.WriteLine($"Path: {string.Join(" ", path)}");
        }

        public static void Main()
        {
            ReadInput();
            BuildGraph();

            var destinationCell = Dijkstra();

            PrintResult(destinationCell);
        }
    }
}
