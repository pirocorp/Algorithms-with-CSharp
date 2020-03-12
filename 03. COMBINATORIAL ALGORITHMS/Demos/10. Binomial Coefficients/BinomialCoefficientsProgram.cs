namespace _10._Binomial_Coefficients
{
    using System;

    public static class BinomialCoefficientsProgram
    {
        private static decimal Binom(int n, int k)
        {
            if (k == 0)
            {
                return 1;
            }

            //SLOW
            return (n * Binom(n - 1, k - 1)) / k;
        }

        public static void Main()
        {
            Console.WriteLine(Binom(49, 6));
        }
    }
}
