namespace LazyRecursion
{
    using System;
    using LazyTypes;
    using Range = LazyTypes.Range;

    public static class EntryPoint
    {
        public static void Main()
        {
            //SideEffectConceptDemo();
            DemoConcept();
            //PairDemoConcept();
            //OptionalDemoConcept();
            //RangeDemoConcept();
            //LazyDemoConcept();
        }

        private static void DemoConcept()
        {
            throw new NotImplementedException();
        }

        static Optional<int> Divide(Lazy<int> x, Lazy<int> y)
        {
            if (y.Value == 0)
            {
                return new Optional<int>();
            }

            return new Optional<int>(new Lazy<int>(() => x.Value / y.Value));
        }

        static Lazy<int> MakeNumber(int number)
        {
            return new Lazy<int>(() => number);
        }

        private static void SideEffectConceptDemo()
        {
            var _42 = new Lazy<int>(() => 42);
            SideEffect.ReadNumber()
                .Bind(x => SideEffect.PrintNumber(_42)
                    .Bind(v => SideEffect.ReadNumber())
                        .Bind(y =>
                        {
                            var sum = new Lazy<int>(() => y.Value + x.Value);
                            return SideEffect.PrintNumber(sum);
                        }))
                .Execute();
        }

        
        private static void RangeDemoConcept()
        {
            new SideEffect<int>(() => 
                    Range.FromTo(MakeNumber(0), MakeNumber(5)).Value.Length())
                .Bind(SideEffect.PrintNumber)
                .Execute();
        }

        private static void OptionalDemoConcept()
        {
            var x = new Lazy<int>(() => 18);
            var y = new Lazy<int>(() => 2);
            var z = new Lazy<int>(() => 3);

            var result = new Optional<int>(x)
                .Bind(v => Divide(v, y))
                .Bind(v => Divide(v, z))
                .WithOptional(
                    new Lazy<int>(() =>
                    {
                        Console.WriteLine("Cannot divide by zero");
                        return 0;
                    }),
                    (v) => new Lazy<int>(() =>
                    {
                        Console.WriteLine($"Result is {v.Value}");
                        return v.Value;
                    }));

            Console.WriteLine(result.Value);
        }

        private static void PairDemoConcept()
        {
            var x = new Lazy<int>(() => 4);
            var y = new Lazy<int>(() => 5);

            var pair = Pair.Make(x, y);
            pair.WithPair<int>((x, y) =>
            {
                Console.WriteLine($"{x.Value} {y.Value}");
                return new Lazy<int>(() => 0);
            });
        }

        private static void LazyDemoConcept()
        {
            var x = new Lazy<int>(() => int.Parse(Console.ReadLine()));
            var y = new Lazy<int>(() => int.Parse(Console.ReadLine()));

            var z = new Lazy<int>(() => x.Value + y.Value);
            Console.WriteLine(z.Value);
        }
    }
}
