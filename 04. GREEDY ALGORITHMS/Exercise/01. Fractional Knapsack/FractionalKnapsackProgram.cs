namespace _01._Fractional_Knapsack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FractionalKnapsackProgram
    {
        private static decimal _capacity;

        public static void Main()
        {
            var materials = ReadInput()
                .OrderByDescending(x => x.Key / x.Value)
                .ToList();

            KnapsackAlgorithm(materials);
        }

        //price, weight
        private static void KnapsackAlgorithm(List<KeyValuePair<decimal, decimal>> materials)
        {
            var index = 0;

            var totalPrice = 0M;

            while (_capacity > 0 && materials.Count > index)
            {
                var current = materials[index++];
                if (current.Value <= _capacity)
                {
                    Console.WriteLine($"Take 100% of item with price {current.Key:F2} and weight {current.Value:F2}");
                    _capacity -= current.Value;
                    totalPrice += current.Key;
                }
                else
                {
                    var fraction = _capacity;
                    Console.WriteLine($"Take {Percent(fraction, current.Value):F2}% of item with price {current.Key:F2} and weight {current.Value:F2}");
                    totalPrice += (current.Key * Percent(fraction, current.Value)) / 100;
                    _capacity -= fraction;
                }
            }

            Console.WriteLine($"Total price: {totalPrice:F2}");
        }

        private static decimal Percent(decimal fraction, decimal whole)
        {
            return (fraction / whole) * 100;
        }

        private static List<KeyValuePair<decimal, decimal>> ReadInput()
        {
            _capacity = int.Parse(Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            var count = int.Parse(Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            //price, weight
            var materials = new List<KeyValuePair<decimal, decimal>>();

            for (var i = 0; i < count; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(decimal.Parse)
                    .ToArray();

                var price = tokens[0];
                var weight = tokens[1];

                materials.Add(new KeyValuePair<decimal, decimal>(price, weight));
            }

            return materials;
        }
    }
}
