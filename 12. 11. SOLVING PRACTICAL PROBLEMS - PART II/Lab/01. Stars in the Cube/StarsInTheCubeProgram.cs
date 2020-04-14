namespace _01._Stars_in_the_Cube
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StarsInTheCubeProgram
    {
        private static List<char[,]> _cube;
        private static SortedDictionary<char, int> _starsCounts;

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());

            _cube = new List<char[,]>();

            for (var i = 0; i < n; i++)
            {
                _cube.Add(new char[n, n]);
            }

            for (var row = 0; row < n; row++)
            {
                var inputRows = Console.ReadLine()
                    .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                for (var layer = 0; layer < n; layer++)
                {
                    var currentRowData = inputRows[layer]
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x[0])
                        .ToArray();

                    for (var col = 0; col < currentRowData.Length; col++)
                    {
                        _cube[layer][row, col] = currentRowData[col];
                    }
                }
            }
        }

        private static bool CheckNextLayer(char currentElement, int row, int col, int nextLayerIndex)
        {
            var nextLayer = _cube[nextLayerIndex];

            if (currentElement != nextLayer[row, col])
            {
                return false;
            }

            if (currentElement != nextLayer[row + 1, col])
            {
                return false;
            }

            if (currentElement != nextLayer[row - 1, col])
            {
                return false;
            }

            if (currentElement != nextLayer[row, col + 1])
            {
                return false;
            }

            if (currentElement != nextLayer[row, col - 1])
            {
                return false;
            }

            return true;
        }

        private static bool CheckForStar(char currentElement, int row, int col, int layerIndex)
        {
            var nextLayerIndex = layerIndex + 1;
            var nextLayer = CheckNextLayer(currentElement, row, col, nextLayerIndex);

            var layerAfterNext = _cube[layerIndex + 2];

            return nextLayer && layerAfterNext[row, col] == currentElement;
        }

        private static void FindStars()
        {
            _starsCounts = new SortedDictionary<char, int>();

            for (var layerIndex = 0; layerIndex < _cube.Count - 2; layerIndex++)
            {
                var currentLayer = _cube[layerIndex];

                for (var row = 1; row < currentLayer.GetLength(0) - 1; row++)
                {
                    for (var col = 1; col < currentLayer.GetLength(1) - 1; col++)
                    {
                        var currentElement = currentLayer[row, col];

                        if (CheckForStar(currentElement, row, col, layerIndex))
                        {
                            if (!_starsCounts.ContainsKey(currentElement))
                            {
                                _starsCounts[currentElement] = 0;
                            }

                            _starsCounts[currentElement]++;
                        }
                    }
                }
            }
        }

        public static void Main()
        {
            ReadInput();

            FindStars();

            var totalStars = _starsCounts.Sum(x => x.Value);

            Console.WriteLine(totalStars);

            foreach (var star in _starsCounts)
            {
                Console.WriteLine($"{star.Key} -> {star.Value}");
            }
        }
    }
}