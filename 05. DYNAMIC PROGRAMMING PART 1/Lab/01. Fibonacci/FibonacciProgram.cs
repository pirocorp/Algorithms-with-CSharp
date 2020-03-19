namespace _01._Fibonacci
{
    using System;

    public static class FibonacciProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var result = IterativeFib(n);
            Console.WriteLine(result);
        }

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
                result[i] = result[i - 1] + result[i - 2];
            }

            return result[n - 1];
        }
    }
}
