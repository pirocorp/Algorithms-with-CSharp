namespace _01._Sorts
{
    using System;
    using System.Collections.Generic;

    public static class Sorts<T> where T : IComparable
    {
        private static IList<T> _collection;
        private static T[] _tempCollection;

        public static void MergeSort(IList<T> collection)
        {
            //Worst Case: n*log(n), Average Case: n*log(n), Best Case: n*log(n)
            //Out of place sort
            _collection = collection;
            _tempCollection = new T[collection.Count];

            Split(0, collection.Count - 1);
        }

        private static void Split(int low, int high)
        {
            if (low == high)
            {
                return;
            }

            var mid = (high + low) / 2;

            Split(low, mid);
            Split(mid + 1, high);

            Merge(low, mid, high);
        }

        private static void Merge(int low, int mid, int high)
        {
            var i = low;
            var j = mid + 1;
            var tempPos = low;

            while (i <= mid && j <= high)
            {
                if (Less(_collection[j], _collection[i]))
                {
                    _tempCollection[tempPos++] = _collection[i++];
                }
                else
                {
                    _tempCollection[tempPos++] = _collection[j++];
                }
            }

            while (i <= mid)
            {
                _tempCollection[tempPos++] = _collection[i++];
            }

            while (j <= high)
            {
                _tempCollection[tempPos++] = _collection[j++];
            }

            for (tempPos = low; tempPos <= high; tempPos++)
            {
                _collection[tempPos] = _tempCollection[tempPos];
            }
        }

        public static void InsertionSort(IList<T> collection)
        {
            //Worst Case: n^2, Average Case: n^2, Best Case: n
            for (var i = 1; i < collection.Count; i++)
            {
                var index = i;

                while (index > 0 &&
                       Less(collection[index - 1], collection[index]))
                {
                    Swap(collection, index, index - 1);
                    index--;
                }
            }
        }

        public static void BubbleSort(IList<T> collection)
        {
            //Worst Case: n^2, Average Case: n^2, Best Case: n
            var swapped = true;
            var end = collection.Count;

            while (swapped)
            {
                swapped = false;

                for (var index = 1; index < end; index++)
                {
                    var prev = collection[index - 1];
                    var current = collection[index];

                    if (Less(prev, current))
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
                    if (Less(min, collection[swapIndex]))
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

        private static bool Less(IComparable first, IComparable second)
        {
            return first.CompareTo(second) > 0;
        }
    }
}
