namespace _02._Rectangle_Intersection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RectangleIntersectionProgram
    {
        private static bool[,] _visited;
        private static bool[,] _intersect;

        private static List<Rectangle>[] _rectangles;
        private static List<int> _xCoordinates;

        private static void SlowSolution()
        {
            _visited = new bool[2001, 2001];
            _intersect = new bool[2001, 2001];
            var count = 0;

            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var coordinates = Console.ReadLine()
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var minX = coordinates[0] + 1000;
                var maxX = coordinates[1] + 1000;
                var minY = coordinates[2] + 1000;
                var maxY = coordinates[3] + 1000;

                for (var row = minX; row < maxX; row++)
                {
                    for (var col = minY; col < maxY; col++)
                    {
                        if (!_visited[row, col])
                        {
                            _visited[row, col] = true;
                        }
                        else
                        {
                            if (!_intersect[row, col])
                            {
                                count++;
                                _intersect[row, col] = true;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            var inputRectangles = new List<Rectangle>();
            _xCoordinates = new List<int>();

            for (var i = 0; i < n; i++)
            {
                var coordinates = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                //Extract all X coordinates x[] from all rectangles (minX and maxX)
                _xCoordinates.Add(coordinates[0]);
                _xCoordinates.Add(coordinates[1]);
                inputRectangles.Add(new Rectangle(coordinates[0], coordinates[1], coordinates[2], coordinates[3]));
            }

            //and sort them in increasingly
            _xCoordinates.Sort();
            _rectangles = new List<Rectangle>[_xCoordinates.Count - 1];

            for (var i = 0; i < _xCoordinates.Count - 1; i++)
            {
                _rectangles[i] = new List<Rectangle>();
            }

            foreach (var rectangle in inputRectangles)
            {
                for (var i = 0; i < _rectangles.Count(); i++)
                {
                    if (rectangle.MaxX > _xCoordinates[i] && rectangle.MinX < _xCoordinates[i + 1])
                    {
                        //For each two coordinates x[i] and x[i+1] find all rectangles rects[] that overlap with this interval
                        _rectangles[i].Add(rectangle);
                    }
                }
            }
        }

        private static long FasterSolution()
        {
            long result = 0;

            for (var i = 0; i < _rectangles.Count(); i++)
            {
                if (_rectangles[i].Count() < 2)
                {
                    continue;
                }

                var yCoordinates = new List<int>();

                foreach (var rectangle in _rectangles[i])
                {
                    //o	Extract all Y coordinates y[] from all rectangles rect[] (minY and maxY)
                    yCoordinates.Add(rectangle.MinY);
                    yCoordinates.Add(rectangle.MaxY);
                }

                //and sort them in increasing order
                yCoordinates.Sort();

                var overlapped = new int[yCoordinates.Count - 1];

                //For each two coordinates y[i] and y[i+1] find how many rectangles overlap with this interval
                foreach (var rectangle in _rectangles[i])
                {
                    for (var j = 0; j < yCoordinates.Count; j++)
                    {
                        if (rectangle.MaxY <= yCoordinates[j] || rectangle.MinY >= yCoordinates[j + 1])
                        {
                            continue;
                        }

                        overlapped[j]++;
                    }
                }

                for (var j = 0; j < overlapped.Count(); j++)
                {
                    //calculate the area where rect_count ≥ 2 and sum it
                    if (overlapped[j] >= 2)
                    {
                        var xSide = _xCoordinates[i + 1] - _xCoordinates[i];
                        var ySide = yCoordinates[j + 1] - yCoordinates[j];
                        result += xSide * ySide;
                    }
                }
            }

            return result;
        }

        public static void Main()
        {
            //SlowSolution();

            ReadInput();
            var result = FasterSolution();
            Console.WriteLine(result);
        }
    }
}
