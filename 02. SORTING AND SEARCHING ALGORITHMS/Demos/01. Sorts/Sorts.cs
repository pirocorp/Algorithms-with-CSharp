namespace _01._Sorts
{
    using System;
    using System.Collections.Generic;

    public static class Sorts<T> where T : IComparable
    {
        public static void BubbleSort(IList<T> collection)
        {
            var swapped = true;
            var end = collection.Count;

            while (swapped)
            {
                swapped = false;

                for (var index = 1; index < end; index++)
                {
                    var prev = collection[index - 1];
                    var current = collection[index];

                    if (ReOrder(prev, current))
                    {
                        Swap(collection, index - 1, index);
                        swapped = true;
                    }
                }

                end--;
            }
        }

        public static void SelectionSort(IList<T> collection)
        {
            //Worst Case: n^2, Average Case: n^2, Best Case: n^2
            for (var currentIndex = 0; currentIndex < collection.Count; currentIndex++)
            {
                var min = collection[currentIndex];
                var swap = -1;

                for (var swapIndex = currentIndex + 1; swapIndex < collection.Count; swapIndex++)
                {
                    if (ReOrder(min, collection[swapIndex]))
                    {
                        swap = swapIndex;
                        min = collection[swapIndex];
                    }
                }

                if (swap != -1)
                {
                    Swap(collection, currentIndex, swap);
                }
            }
        }

        private static void Swap(IList<T> collection, int from, int to)
        {
            var swap = collection[from];
            collection[from] = collection[to];
            collection[to] = swap;
        }

        private static bool ReOrder(IComparable first, IComparable second)
        {
            return first.CompareTo(second) > 0;
        }
    }
}
