namespace _03._Binary_Search
{
    using System;
    using System.Collections.Generic;

    public static class BinarySearchProgram
    {
        public static void Main()
        {
            var collection = new List<int>();

            for (var i = 1; i <= 30; i++)
            {
                collection.Add(i);
            }

            var key = 23;

            var index = BinarySearch(collection, key, 0, collection.Count - 1);

            if (index != -1)
            {
                Console.WriteLine(collection[index]);
            }
            else
            {
                Console.WriteLine("Not found.");
            }
        }

        private static int BinarySearch<T>(List<T> collection, T key, int start, int end) where T : IComparable
        {
            if (end < start)
            {
                return -1;
            }
            else
            {
                var mid = (start + end) / 2;
                if (Less(collection[mid], key))
                {
                    return BinarySearch<T>(collection, key, start, mid - 1);
                }
                else if(Less(key, collection[mid]))
                {
                    return BinarySearch<T>(collection, key, mid + 1, end);
                }
                else
                {
                    return mid;
                }
            }
        }

        private static bool Less(IComparable first, IComparable second)
        {
            return first.CompareTo(second) > 0;
        }
    }
}
