namespace _05._Egyptian_Fractions
{
    public class Fraction
    {
        public Fraction(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public int Numerator { get; }

        public int Denominator { get; }

        public override string ToString()
        {
            return $"{this.Numerator}/{this.Denominator}";
        }
    }
}
