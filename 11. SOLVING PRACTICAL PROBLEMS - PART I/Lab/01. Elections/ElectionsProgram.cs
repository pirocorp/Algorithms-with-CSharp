namespace _01._Elections
{
    using System;
    using System.Linq;
    using System.Numerics;

    public static class ElectionsProgram
    {
        private static int _majority;
        private static int[] _parties;
        private static int _coalitionsCount;

        private static BigInteger[] _solutions;

        private static void ReadInput()
        {
            _coalitionsCount = 0;
            _majority = int.Parse(Console.ReadLine());
            var count = int.Parse(Console.ReadLine());

            _parties = new int[count];

            for (var i = 0; i < _parties.Length; i++)
            {
                _parties[i] = int.Parse(Console.ReadLine());
            }
        }

        private static void CountPossibleCoalitions(int[] vector, int index, int border)
        {
            if (index >= vector.Length)
            {
                if (vector.Sum() >= _majority)
                {
                    _coalitionsCount++;
                }
            }
            else
            {
                for (var i = border; i < _parties.Length; i++)
                {
                    vector[index] = _parties[i];
                    CountPossibleCoalitions(vector, index + 1, i + 1);
                }
            }
        }

        private static void SlowSolution()
        {
            for (var i = 1; i <= _parties.Length; i++)
            {
                CountPossibleCoalitions(new int[i], 0, 0);
            }

            Console.WriteLine(_coalitionsCount);
        }

        private static void FastSolution()
        {
            var totalMaxSum = _parties.Sum();
            _solutions = new BigInteger[totalMaxSum + 1];
            _solutions[0] = 1;

            foreach (var party in _parties)
            {
                for (var i = _solutions.Length - 1; i >= 0; i--)
                {
                    if (_solutions[i] > 0)
                    {
                        _solutions[i + party] += _solutions[i];
                    }
                }
            }

            BigInteger result = 0;

            for (var i = _majority; i < _solutions.Length; i++)
            {
                result += _solutions[i];
            }

            Console.WriteLine(result);
        }

        public static void Main()
        {
            ReadInput();
            //SlowSolution();
            FastSolution();
        }
    }
}
