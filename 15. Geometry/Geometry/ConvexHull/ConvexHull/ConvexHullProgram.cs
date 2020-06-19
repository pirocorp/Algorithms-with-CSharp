namespace ConvexHull
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class ConvexHullProgram
    {
        private const double EPSILON = 1e-12;

        public static void Main()
        {
            var rnd = new Random(42);

            var points = Enumerable
                .Range(0, 10_000)
                .Select(x => Point.GeneratePoint(rnd))
                .ToArray();

            //Worst Case Simple Solution
            for (var i = 0; i < points.Length; i++)
            {
                var angle = i * 2 * Math.PI / points.Length;
                points[i].X = Math.Cos(angle) * 100;
                points[i].Y = Math.Sin(angle) * 100;
            }

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var hull = GetPointsOfConvexHullSimpleSolution(points);
            stopwatch.Stop();

            Console.WriteLine("Simple solution: ");
            //Console.WriteLine(string.Join(" ", hull));
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");

            Console.WriteLine();
            stopwatch.Reset();

            stopwatch.Start();
            hull = ConvexHullGrahamScan(points);
            stopwatch.Stop();

            Console.WriteLine("Graham Scan solution: ");
            //Console.WriteLine(string.Join(" ", hull));
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
            
            Console.WriteLine();
            stopwatch.Reset();

            stopwatch.Start();
            hull = ConvexHullGrahamScan2(points);
            stopwatch.Stop();

            Console.WriteLine("Graham Scan 2 solution: ");
            //Console.WriteLine(string.Join(" ", hull));
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
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

        private static List<Point> ConvexHullTest()
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

            return points.ToList();
        }
        
        private static int[] GetSortedPointsIndexes(Point[] points, int firstIndex)
        {
            var sortedPoints = Enumerable
                .Range(0, points.Length - 1)
                .ToArray();

            if (firstIndex < sortedPoints.Length)
            {
                sortedPoints[firstIndex] = points.Length - 1;
            }

            var firstPoint = points[firstIndex];

            Array.Sort(sortedPoints, (ai, bi) =>
            {
                var a = points[ai];
                var b = points[bi];

                var area = DirectedArea(firstPoint, a, b);

                if (area < -EPSILON)
                {
                    return 1;
                }
                else if (area > EPSILON)
                {
                    return -1;
                }
                
                return 0;
            });

            return sortedPoints;
        }

        private static (Point First, Point Second) GetLatestTwoPoints(List<int> hull, Point[] points)
        {
            var secondIndex = hull[^1];
            var firstIndex = hull[^2];

            var secondPoint = points[secondIndex];
            var firstPoint = points[firstIndex];

            return (firstPoint, secondPoint);
        }

        /// <summary>
        /// O(n) after points are sorted
        /// </summary>
        /// <param name="points">Points Collection</param>
        /// <returns></returns>
        private static List<int> ConvexHullGrahamScan(Point[] points)
        {
            var hull = new List<int>();

            var firstIndex = GetLowestPointIndex(points);

            var sortedPointsIndexes = GetSortedPointsIndexes(points, firstIndex);

            hull.Add(firstIndex);
            hull.Add(sortedPointsIndexes[0]);

            for (var index = 1; index < sortedPointsIndexes.Length; index++)
            {
                var currentPointIndex = sortedPointsIndexes[index];
                var currentPoint = points[currentPointIndex];

                var (first, second) = GetLatestTwoPoints(hull, points);

                while (true)
                {
                    var area = DirectedArea(first, second, currentPoint);

                    if (area < EPSILON)
                    {
                        hull.RemoveLast();
                    }
                    else
                    {
                        hull.Add(currentPointIndex);
                        break;
                    }

                    (first, second) = GetLatestTwoPoints(hull, points);
                }
            }

            return hull;
        }

        private static void SelectPoint(Point[] points, List<int> hull, int sortedIndex)
        {
            while (hull.Count > 1)
            {
                var area = DirectedArea(
                    points[hull[^2]],
                    points[hull[^1]],
                    points[sortedIndex]);

                if (area > EPSILON)
                {
                    break;
                }

                hull.RemoveLast();
            }

            hull.Add(sortedIndex);
        }

        /// <summary>
        /// O(n) using alternative sort method
        /// </summary>
        /// <param name="points">Points Collection</param>
        /// <returns></returns>
        private static List<int> ConvexHullGrahamScan2(Point[] points)
        {
            var hull = new List<int>();

            var sortedIndexes = Enumerable
                .Range(0, points.Length)
                .OrderBy(x => points[x].Y)
                .ToArray();

            hull.Add(sortedIndexes[0]);
            hull.Add(sortedIndexes[1]);

            for (var i = 2; i < sortedIndexes.Length; i++)
            {
                SelectPoint(points, hull, sortedIndexes[i]);
            }

            for (var i = sortedIndexes.Length - 2; i >= 0; i--)
            {
                SelectPoint(points, hull, sortedIndexes[i]);
            }

            hull.RemoveLast();
            return hull;
        }
    }
}
