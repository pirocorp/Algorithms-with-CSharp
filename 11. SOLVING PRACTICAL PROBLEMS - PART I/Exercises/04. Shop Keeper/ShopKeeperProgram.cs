namespace _04._Shop_Keeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ShopKeeperProgram
    {
        private static HashSet<int> _stock;
        private static List<int> _orders;
        private static Dictionary<int, LinkedList<int>> _nextOrderIndex;

        private static void ReadInput()
        {
            var sequenceStock = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            _stock = new HashSet<int>();

            foreach (var i in sequenceStock)
            {
                _stock.Add(i);
            }

            _orders = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            _nextOrderIndex = new Dictionary<int, LinkedList<int>>();

            for (var index = 0; index < _orders.Count; index++)
            {
                var currentOrder = _orders[index];

                if (!_nextOrderIndex.ContainsKey(currentOrder))
                {
                    _nextOrderIndex[currentOrder] = new LinkedList<int>();
                }

                _nextOrderIndex[currentOrder].AddLast(index);
            }
        }

        private static void CalculateSwaps()
        {
            if (!_stock.Contains(_orders[0]))
            {
                Console.WriteLine("impossible");
                return;
            }

            var count = 0;

            for (var i = 0; i < _orders.Count; i++)
            {
                var currentOrder = _orders[i];

                if (_stock.Contains(currentOrder))
                {
                    _nextOrderIndex[currentOrder].RemoveFirst();
                    continue;
                }

                _nextOrderIndex[currentOrder].RemoveFirst();

                var swap = _nextOrderIndex
                    .Where(x => x.Value.Count == 0)
                    .Select(x => x.Key)
                    .FirstOrDefault();

                if (swap == 0)
                {
                    swap = _nextOrderIndex
                        .Where(x => _stock.Contains(x.Key))
                        .OrderByDescending(x => x.Value.First.Value)
                        .First()
                        .Key;
                }

                count++;
                _stock.Remove(swap);
                _stock.Add(currentOrder);
            }

            Console.WriteLine(count);
        }

        public static void Main()
        {
            ReadInput();
            CalculateSwaps();

        }
    }
}
