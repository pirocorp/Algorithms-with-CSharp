namespace _01._Knapsack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class KnapsackProgram
    {
        private static int _maxCapacity;
        private static List<Item> _items;
        private static int[,] _prices;
        private static bool[,] _itemsIncluded;

        public static void Main()
        {
            ReadInput();
            CalculatePrices();
            List<Item> result = ReconstructSolution();

            Console.WriteLine($"Total Weight: {result.Sum(x => x.Weight)}");
            Console.WriteLine($"Total Value: {_prices[_items.Count, _maxCapacity]}");
            Console.WriteLine(string.Join(Environment.NewLine, result
                .Select(x => x.Name)
                .OrderBy(x => x)
                .ToArray()));
        }

        private static List<Item> ReconstructSolution()
        {
            int capacity = _maxCapacity;

            var result = new List<Item>();

            for (int itemIndex = _items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                if (_itemsIncluded[itemIndex + 1, capacity])
                {
                    var currentItem = _items[itemIndex];
                    result.Add(currentItem);
                    capacity -= currentItem.Weight;
                }
            }

            return result;
        }

        private static void CalculatePrices()
        {
            _prices = new int[_items.Count + 1, _maxCapacity + 1];
            _itemsIncluded = new bool[_items.Count + 1, _maxCapacity + 1];

            for (int itemIndex = 0; itemIndex < _items.Count; itemIndex++)
            {
                var item = _items[itemIndex];
                var rowIndex = itemIndex + 1;

                for (int capacity = 0; capacity <= _maxCapacity; capacity++)
                {
                    if (item.Weight > capacity)
                    {
                        continue;
                    }

                    int excluding = _prices[rowIndex - 1, capacity];
                    int including = item.Price + _prices[rowIndex - 1, capacity - item.Weight];

                    if (including > excluding)
                    {
                        _prices[rowIndex, capacity] = including;
                        _itemsIncluded[rowIndex, capacity] = true;
                    }
                    else
                    {
                        _prices[rowIndex, capacity] = excluding;
                    }
                }
            }
        }

        private static void ReadInput()
        {
            _maxCapacity = int.Parse(Console.ReadLine());

            _items = new List<Item>();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "end")
                {
                    break;
                }

                var parts = line.Split(' ');
                var newItem = new Item(parts);
                _items.Add(newItem);
            }
        }
    }
}
