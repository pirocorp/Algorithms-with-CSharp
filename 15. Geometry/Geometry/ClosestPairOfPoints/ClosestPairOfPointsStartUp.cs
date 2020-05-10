namespace ClosestPairOfPoints
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Geometry.Objects;

    public static class ClosestPairOfPointsStartUp
    {
        private const double EPSILON = 1e-12;
        private delegate Tuple<int, int> Algorithm(Point[] points);

        public static void Main()
        {
            var rnd = new ConsistentRandom();

            var points = Enumerable.Range(0, 30_000)
                .Select(x => Point.GeneratePoint(rnd))
                //.Select(p =>
                //{
                //    p.X = 1;
                //    return p;
                //})
                .ToArray();

            AlgorithmTester(points, FindClosestPairNaive);

            AlgorithmTester(points, FindClosestPairDivideConquer);
        }

        private static void AlgorithmTester(Point[] points, Algorithm algorithm)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var (firstPointIndex, secondPointIndex) = algorithm(points);
            stopWatch.Stop();
            var distance = points[firstPointIndex].DistanceTo(points[secondPointIndex]);

            Console.WriteLine($"{firstPointIndex} {secondPointIndex} {distance}");
            Console.WriteLine($"{algorithm.Method.Name}: {stopWatch.Elapsed}");
            Console.WriteLine(new string('-', Console.WindowWidth));
        }

        /// <summary>
        /// Worst Case: O(n^2)
        /// Best Case: O(n^2)
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Tuple<int, int> FindClosestPairNaive(Point[] points)
        {
            var bestFirst = 0;
            var bestSecond = 1;
            var bestDistance = points[0].DistanceToSquared(points[1]);

            for (var i = 0; i < points.Length; i++)
            {
                for (var j = i + 1; j < points.Length; j++)
                {
                    var currentDistance = points[i].DistanceToSquared(points[j]);

                    if (bestDistance > currentDistance)
                    {
                        bestDistance = currentDistance;
                        bestFirst = i;
                        bestSecond = j;
                    }
                }
            }

            return new Tuple<int, int>(bestFirst, bestSecond);
        }

        /// <summary>
        /// Worst Case: O(n*log(n)^2)
        /// Best Case: O(n*log(n))
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Tuple<int, int> FindClosestPairDivideConquer(
            Point[] points)
        {
            Array.Sort(points, Point.XComparer);
            return FindClosestPairDivideConquer(points, 0, points.Length);
        }

        /// <summary>
        /// Actual recursive solution
        /// </summary>
        /// <param name="points"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static Tuple<int, int> FindClosestPairDivideConquer(
            Point[] points, int begin, int end)
        {
            if (end - begin == 2)
            {
                return new Tuple<int, int>(begin, begin + 1);
            }

            if (end - begin < 2)
            {
                return null;
            }

            var middle = (begin + end) / 2;
            var leftResult = FindClosestPairDivideConquer(points, begin, middle);
            var rightResult = FindClosestPairDivideConquer(points, middle, end);

            var leftDistance = leftResult == null
                ? double.PositiveInfinity
                : points[leftResult.Item1].DistanceTo(points[leftResult.Item2]);
            
            var rightDistance = rightResult == null
                ? double.PositiveInfinity
                : points[rightResult.Item1].DistanceTo(points[rightResult.Item2]);
            
            var bestDistance = leftDistance < rightDistance
                ? leftDistance
                : rightDistance;

            var bestPair = leftDistance < rightDistance
                ? leftResult
                : rightResult;

            var borderMiddle = points[middle].X;
            var borderLeft = borderMiddle - bestDistance;
            var borderRight = borderMiddle + bestDistance;

            var borderLeftIndex = FindPointX(points, begin, middle, borderLeft);
            var borderRightIndex = FindPointX(points, middle, end, borderRight);

            var leftPoints = 
                 Enumerable.Range(borderLeftIndex, middle - borderLeftIndex)
                .OrderBy(index => points[index].Y)
                .ToArray();

            var rightPoints = 
                Enumerable.Range(middle, borderRightIndex - middle)
                .OrderBy(index => points[index].Y)
                .ToArray();


            for (int i = 0, j = 0; i < leftPoints.Length; i++)
            {
                var yBottomLine = points[leftPoints[i]].Y - bestDistance;
                var yTopLine = points[leftPoints[i]].Y + bestDistance;

                while (   j < rightPoints.Length 
                       && points[rightPoints[j]].Y < yBottomLine - EPSILON)
                {
                    ++j;
                }

                if (j >= rightPoints.Length)
                {
                    break;
                }

                for (var k = j; k < rightPoints.Length; k++)
                {
                    if (points[rightPoints[k]].Y > yTopLine + EPSILON)
                    {
                        break;
                    }

                    var currentDistance =
                        points[leftPoints[i]].DistanceTo(points[rightPoints[k]]);

                    if (bestDistance > currentDistance)
                    {
                        bestDistance = currentDistance;
                        bestPair = new Tuple<int, int>(leftPoints[i], rightPoints[k]);
                        yTopLine = points[leftPoints[i]].Y + bestDistance;
                    }
                }
            }

            return bestPair;
        }

        /// <summary>
        /// Binary Search
        /// </summary>
        /// <param name="points"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int FindPointX(Point[] points, int left, int right, 
            double x)
        {
            while (left < right)
            {
                var middle = (left + right) / 2;

                if (points[middle].X < x)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle;
                }
            }

            return left;
        }
    }
}
