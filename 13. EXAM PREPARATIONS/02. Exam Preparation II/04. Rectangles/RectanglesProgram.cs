﻿namespace _04._Rectangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RectanglesProgram
    {
        private static List<RectAngle> _rectAngles;

        private static int[] _lns;
        private static int[] _prev;

        private static void ReadInput()
        {
            _rectAngles = new List<RectAngle>();

            var inputLine = Console.ReadLine();

            while (inputLine != "End")
            {
                var rectangleArgs = inputLine
                    .Split(": ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var name = rectangleArgs[0];
                var x1 = int.Parse(rectangleArgs[1]);
                var y1 = int.Parse(rectangleArgs[2]);
                var x2 = int.Parse(rectangleArgs[3]);
                var y2 = int.Parse(rectangleArgs[4]);

                var rect = new RectAngle(name, x1, y1, x2, y2);
                _rectAngles.Add(rect);

                inputLine = Console.ReadLine();
            }

            _rectAngles = _rectAngles
                .OrderByDescending(x => x.Size)
                .ToList();
        }

        private static void GetLongestNestedSubsequenceDp()
        {
            _lns = new int[_rectAngles.Count];
            _prev = Enumerable.Repeat(-1, _rectAngles.Count).ToArray();

            for (var currentIndex = 0; currentIndex < _rectAngles.Count; currentIndex++)
            {
                var element = _rectAngles[currentIndex];

                var maxLen = 1;
                var prev = -1;

                for (var prevIndex = 0; prevIndex < currentIndex; prevIndex++)
                {
                    var prevElement = _rectAngles[prevIndex];
                    var prevSolutionLength = _lns[prevIndex];

                    if (prevElement.isNested(element))
                    {
                        if (maxLen <= prevSolutionLength)
                        {
                            maxLen = prevSolutionLength + 1;
                            prev = prevIndex;
                        }

                        //Alphabetical Order
                        if (maxLen == prevSolutionLength + 1)
                        {
                            if (string.Compare(_rectAngles[prev].Name, _rectAngles[prevIndex].Name, StringComparison.InvariantCulture) > 0)
                            {
                                prev = prevIndex;
                            }
                        }
                    }
                }

                _lns[currentIndex] = maxLen;
                _prev[currentIndex] = prev;
            }
        }

        private static List<string> ReconstructSolution(int i)
        {
            var result = new Stack<string>();

            while (i != -1)
            {
                result.Push(_rectAngles[i].Name);
                i = _prev[i];
            }

            return result.ToList();
        }

        private static void LongestNestedSubSequenceSolution()
        {
            ReadInput();

            GetLongestNestedSubsequenceDp();

            var max = _lns.Max();

            var solutions = new List<List<string>>();

            for (var i = 0; i < _lns.Length; i++)
            {
                if (_lns[i] == max)
                {
                    var solution = ReconstructSolution(i);
                    solutions.Add(solution);
                }
            }

            var result = solutions
                .Select(x => string.Join(" < ", x))
                .OrderBy(x => x)
                .First();

            Console.WriteLine(result);
        }

        public static void FindNestedRectangles(RectAngle rect)
        {
            if (rect.Depth > 0)
            {
                return;
            }

            RectAngle bestNested = null;

            for (var i = 0; i < _rectAngles.Count; i++)
            {
                var other = _rectAngles[i];

                if (rect.isNested(other) && other != rect)
                {
                    FindNestedRectangles(other);

                    //CompareTo is cultureSpecific
                    if (bestNested == null
                        || other.Depth > bestNested.Depth
                        || other.Depth == bestNested.Depth
                        && string.Compare(other.Name, bestNested.Name, StringComparison.InvariantCulture) < 0)
                    {
                        bestNested = other;
                    }
                }
            }

            rect.Depth = (bestNested?.Depth ?? 0) + 1;
            rect.Nested = bestNested;
        }

        public static void SweepAndPrune(RectAngle rect, int index)
        {
            if (rect.Depth > 0)
            {
                return;
            }

            RectAngle bestNested = null;

            for (var i = index + 1; i < _rectAngles.Count; i++)
            {
                var other = _rectAngles[i];

                if (other.X1 > rect.X2)
                {
                    break;
                }

                if (rect.isNested(other))
                {
                    SweepAndPrune(other, i);

                    if (bestNested == null
                        || other.Depth > bestNested.Depth
                        || other.Depth == bestNested.Depth
                        && string.Compare(other.Name, bestNested.Name, StringComparison.InvariantCulture) < 0)
                    {
                        bestNested = other;
                    }
                }
            }

            rect.Depth = (bestNested?.Depth ?? 0) + 1;
            rect.Nested = bestNested;
        }

        private static void PrintResult()
        {
            var best = _rectAngles
                .OrderByDescending(x => x.Depth)
                .ThenBy(x => x.Name)
                .First();

            var result = new List<RectAngle>();

            while (best != null)
            {
                result.Add(best);
                best = best.Nested;
            }

            Console.WriteLine(string.Join(" < ", result.Select(x => x.Name)));
        }

        private static void RecursiveSolution()
        {
            ReadInput();

            for (var i = 0; i < _rectAngles.Count; i++)
            {
                var rect = _rectAngles[i];
                FindNestedRectangles(rect);
            }

            PrintResult();
        }

        private static void SweepAndPruneSolution()
        {
            ReadInput();

            _rectAngles = _rectAngles
                .OrderBy(x => x.X1)
                .ThenByDescending(x => x.X2)
                .ThenByDescending(y => y.Y1)
                .ThenBy(y => y.Y2)
                .ToList();

            for (var i = 0; i < _rectAngles.Count; i++)
            {
                var rect = _rectAngles[i];
                SweepAndPrune(rect, i);
            }

            PrintResult();
        }

        public static void Main()
        {
            //LongestNestedSubSequenceSolution();
            //RecursiveSolution();
            SweepAndPruneSolution();
        }
    }
}
