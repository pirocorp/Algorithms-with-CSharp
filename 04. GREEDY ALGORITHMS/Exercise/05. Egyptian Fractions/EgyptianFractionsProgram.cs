namespace _05._Egyptian_Fractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EgyptianFractionsProgram
    {
        public static void Main()
        {
            var tokens = Console.ReadLine()
                .Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var numerator = tokens[0];
            var denominator = tokens[1];

            if (numerator >= denominator)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            var result = new List<Fraction>();

            while (numerator != 0)
            {
                var newDenominator = (int)Math.Ceiling(denominator / (double)numerator);
                var newFraction = new Fraction(1, newDenominator);
                result.Add(newFraction);

                numerator = numerator * newDenominator - denominator;
                denominator = denominator * newDenominator;

                var gcf = GreatestCommonFactor(numerator, denominator);

                while (gcf > 1)
                {
                    numerator /= gcf;
                    denominator /= gcf;

                    gcf = GreatestCommonFactor(numerator, denominator);
                }
            }

            Console.WriteLine($"{tokens[0]}/{tokens[1]} = {string.Join(" + ", result)}");
        }

        private static int GreatestCommonFactor(int a, int b)
        {
            if (a == b)
            {
                return a;
            }

            int min;
            int max;

            if (Math.Abs(a) < Math.Abs(b))
            {
                min = a;
                max = b;
            }
            else
            {
                min = b;
                max = a;
            }

            for (var i = Math.Abs(min); i > 0; i--)
            {
                if (a % i == 0 && 
                    b % i == 0)
                {
                    return i;
                }
            }

            return 1;
        }
    }
}
