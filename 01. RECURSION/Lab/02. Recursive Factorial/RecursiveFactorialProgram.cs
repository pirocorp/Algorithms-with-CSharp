namespace _02._Recursive_Factorial
{
    using System;

    public static class RecursiveFactorialProgram
    {
        public static void Main()
        {
            var factor = int.Parse(Console.ReadLine());
            var result = Factor(factor);
            Console.WriteLine(result);
        }

        private static long Factor(int factor)
        {
            if (factor == 1)
            {
                return 1;
            }

            return factor * Factor(factor - 1);
        }
    }
}
