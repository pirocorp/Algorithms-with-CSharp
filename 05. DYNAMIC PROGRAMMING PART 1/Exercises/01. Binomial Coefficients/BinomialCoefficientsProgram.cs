namespace _01._Binomial_Coefficients
{
    using System;
    using System.Collections.Generic;

    public static class BinomialCoefficientsProgram
    {
        public static void Main()
        {
            var row = long.Parse(Console.ReadLine());
            var col = long.Parse(Console.ReadLine());

            //Dictionary<row, Dictionary<col, coefficient>>
            var coefficients = new Dictionary<long, Dictionary<long, long>>();

            var result = CalculateBinomialCoefficient(row, col, coefficients);

            Console.WriteLine(result);
        }

        private static long CalculateBinomialCoefficient(long row, long col, Dictionary<long, Dictionary<long, long>> coefficients)
        {
            if (!coefficients.ContainsKey(row))
            {
                coefficients[row] = new Dictionary<long, long>();
            }

            if (coefficients[row].ContainsKey(col))
            {
                return coefficients[row][col];
            }

            if (col > row)
            {
                return 0;
            }

            if (col == 0 || col == row)
            {
                return 1;
            }

            var left = CalculateBinomialCoefficient(row - 1, col - 1, coefficients);
            var right = CalculateBinomialCoefficient(row - 1, col, coefficients);
            var result = left + right;

            coefficients[row][col] = result;

            return coefficients[row][col];
        }
    }
}
