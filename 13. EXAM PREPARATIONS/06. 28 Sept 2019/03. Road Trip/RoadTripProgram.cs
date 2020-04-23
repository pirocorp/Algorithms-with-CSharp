namespace _03._Road_Trip
{
    using System;
    using System.Linq;

    public static class RoadTripProgram
    {
        private static int[] _values;
        private static int[] _amountsOfSpace;
        private static int _maxCapacity;

        private static int[,] _maximumValues;

        private static void ReadInput()
        {
            _values = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            _amountsOfSpace = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            _maxCapacity = int.Parse(Console.ReadLine());
        }

        private static void CalculateValues()
        {
            _maximumValues = new int[_values.Length + 1, _maxCapacity + 1];

            for (var itemIndex = 0; itemIndex < _values.Length; itemIndex++)
            {
                var itemValue = _values[itemIndex];
                var itemSize = _amountsOfSpace[itemIndex];
                var rowIndex = itemIndex + 1;

                for (var capacity = 0; capacity <= _maxCapacity; capacity++)
                {
                    var excluding = _maximumValues[rowIndex - 1, capacity];

                    if (itemSize > capacity)
                    {
                        _maximumValues[rowIndex, capacity] = excluding;
                        continue;
                    }

                    var including = itemValue + _maximumValues[rowIndex - 1, capacity - itemSize];
                    
                    if (including > excluding)
                    {
                        _maximumValues[rowIndex, capacity] = including;
                    }
                    else
                    {
                        _maximumValues[rowIndex, capacity] = excluding;
                    }
                }
            }
        }

        public static void Main()
        {
            ReadInput();
            CalculateValues();
            var max = int.MinValue;

            for (var row = 0; row < _maximumValues.GetLength(0); row++)
            {
                for (var col = 0; col < _maximumValues.GetLength(1); col++)
                {
                    var current = _maximumValues[row, col];

                    if (max < current)
                    {
                        max = current;
                    }
                }
            }

            Console.WriteLine($"Maximum value: {max}");
        }
    }
}
