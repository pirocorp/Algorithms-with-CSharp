namespace _01._Fibonacci
{
    using System;

    public static class FibonacciProgram
    {
        private static int _count;
        private static long[] _numbers;

        private static  long SimpleRecursionFib(int n)
        {
            _count++;

            if (n == 1 || n == 2)
            {
                return 1;
            }

            return SimpleRecursionFib(n - 1) + SimpleRecursionFib(n - 2);
        }

        //Top-down approach
        private static long RecursionWithMemoizationiFib(int n)
        {
            _count++;

            if (_numbers[n] != 0)
            {
                return _numbers[n];
            }

            if (n == 1 || n == 2)
            {
                return 1;
            }

            var result = RecursionWithMemoizationiFib(n - 1) + RecursionWithMemoizationiFib(n - 2);
            _numbers[n] = result;

            return result;
        }

        //Bottom-up approach
        private static long IterativeFib(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            var result = new long[n];
            result[0] = 1;
            result[1] = 1;

            for (var i = 2; i < n; i++)
            {
                _count++;
                result[i] = result[i - 1] + result[i - 2];
            }

            return result[n - 1];
        }

        public static void Main()
        {
            var n = 40;

            _numbers = new long[n + 1];
            Console.WriteLine(SimpleRecursionFib(n));
            Console.WriteLine($"{_count:##,###}");

            Console.WriteLine(new string('-', Console.WindowWidth));

            _count = 0;
            Console.WriteLine(RecursionWithMemoizationiFib(n));
            Console.WriteLine($"{_count:##,###}");

            Console.WriteLine(new string('-', Console.WindowWidth));

            _count = 0;
            Console.WriteLine(IterativeFib(n));
            Console.WriteLine($"{_count:##,###}");
        }
    }
}
