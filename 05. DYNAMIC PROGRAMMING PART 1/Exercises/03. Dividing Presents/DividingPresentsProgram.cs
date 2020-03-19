namespace _03._Dividing_Presents
{
    using System;
    using System.Linq;
    using System.Text;

    public static class DividingPresentsProgram
    {
        private static int[] _presents;
        private static int[] _sums;

        public static void Main()
        {
            ReadInput();
            var total = _presents.Sum();
            InitializeSums(total);
            CalculateSums(total);
            PrintSolution(total);
        }

        private static void CalculateSums(int total)
        {
            //Calculate all possible sums
            for (var current = 0; current < _presents.Length; current++)
            {
                //Finding all possible sum for current value
                for (var prev = total; prev >= 0; prev--)
                {
                    //-1 Sum is still not found
                    if (_sums[prev] != -1 &&
                        _sums[prev + _presents[current]] == -1)
                    {
                        //Can achieve this sum if add current
                        _sums[prev + _presents[current]] = current;
                    }
                }
            }
        }

        private static void ReadInput()
        {
            _presents = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private static void InitializeSums(int total)
        {
            _sums = new int[total + 1];
            
            _sums[0] = 0; //Sentinel bit
            for (var i = 1; i < _sums.Length; i++)
            {
                _sums[i] = -1;
            }
        }

        private static void PrintSolution(int total)
        {
            var half = total / 2;
            for (var i = half; i >= 0; i--)
            {
                if (_sums[i] != -1)
                {
                    var alanShare = i;
                    var bobShare = total - i;

                    Console.WriteLine($"Difference: {bobShare - alanShare}");
                    Console.WriteLine($"Alan:{alanShare} Bob:{bobShare}");
                    Console.WriteLine($"Alan takes: {GetAlanPresents(alanShare)}");
                    Console.WriteLine($"Bob takes the rest.");
                    break;
                }
            }
        }

        private static string GetAlanPresents(int index)
        {
            var result = new StringBuilder();

            //Reconstructs solution
            while (index != 0)
            {
                //Sum is achieved adding this value
                var present = _presents[_sums[index]];
                result.Append($"{present} ");
                index -= present; //previous sum
            }

            return result.ToString().Trim();
        }
    }
}
