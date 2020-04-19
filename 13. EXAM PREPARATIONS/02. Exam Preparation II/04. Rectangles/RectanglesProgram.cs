namespace _04._Rectangles
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

        public static void Main()
        {
            LongestNestedSubSequenceSolution();
            
        }
    }
}
