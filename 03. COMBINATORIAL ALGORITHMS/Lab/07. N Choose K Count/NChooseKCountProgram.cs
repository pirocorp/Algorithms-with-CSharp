namespace _07._N_Choose_K_Count
{
    using System;

    public static class NChooseKCountProgram
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
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());
            Console.WriteLine(Binom(n, k));
        }
    }
}
