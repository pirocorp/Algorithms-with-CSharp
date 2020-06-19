namespace ConvexHull
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConvexHullProgram
    {
        private const double EPSILON = 1e-12;

        public static void Main()
        {
            var rnd = new Random(42);

            var points = Enumerable
                .Range(0, 50)
                .Select(x => Point.GeneratePoint(rnd))
                .ToArray();

            var hull = GetPointsOfConvexHullSimpleSolution(points);

            Console.WriteLine("Simple solution: ");
            Console.WriteLine(string.Join(" ", hull));
        }
        
        private static int GetLowestPointIndex(Point[] points)
        {
            var lowestPointIndex = 0;

            for (var i = 0; i < points.Length; i++)
            {
                if (points[lowestPointIndex].Y > points[i].Y)
                {
                    lowestPointIndex = i;
                }
            }

            return lowestPointIndex;
        }

        private static double DirectedArea(Point a, Point b, Point c)
              => a.X * (b.Y - c.Y)
               + b.X * (c.Y - a.Y)
               + c.X * (a.Y - b.Y);

        private static int GetNextPoint(Point[] points, int prevPoint)
        {
            var nextPoint = 0;

            if (prevPoint == 0)
            {
                nextPoint = 1;
            }

            for (var i = 1; i < points.Length; i++)
            {
                var area = DirectedArea(points[prevPoint], points[i], points[nextPoint]);

                if (area > EPSILON)
                {
                    nextPoint = i;
                }
            }

            return nextPoint;
        }

        /// <summary>
        /// O(n^2)
        /// </summary>
        /// <param name="points">Points Collection</param>
        /// <returns></returns>
        private static List<int> GetPointsOfConvexHullSimpleSolution(Point[] points)
        {
            var pointsIndexes = new List<int>();

            var firstPoint = GetLowestPointIndex(points);
            pointsIndexes.Add(firstPoint);

            var prevPoint = firstPoint;

            while (true)
            {
                var nextPoint = GetNextPoint(points, prevPoint);

                if (nextPoint == firstPoint)
                {
                    break;
                }

                pointsIndexes.Add(nextPoint);
                prevPoint = nextPoint;
            }

            return pointsIndexes;
        }

        private static void ConvexHullTest()
        {
            //(6, -10),(8, -7),(8, 6),(-7, 8),(-10, 4),(-10, 3),(-9, -5) - Correct result
            var points = new[]
            {
                new Point(-7, 8),
                new Point(-4, 6),
                new Point(2, 6),
                new Point(6, 4),
                new Point(8, 6),
                new Point(7, -2),
                new Point(4, -6),
                new Point(8, -7),
                new Point(0, 0),
                new Point(3, -2),
                new Point(6, -10),
                new Point(0, -6),
                new Point(-9, -5),
                new Point(-8, -2),
                new Point(-8, 0),
                new Point(-10, 3),
                new Point(-2, 2),
                new Point(-10, 4)
            };

            var result = GetPointsOfConvexHullSimpleSolution(points);

            Console.WriteLine("Simple solution result:");

            foreach (var i in result)
            {
                Console.Write(points[i] + " ");
            }

            Console.WriteLine();
        }
    }
}
