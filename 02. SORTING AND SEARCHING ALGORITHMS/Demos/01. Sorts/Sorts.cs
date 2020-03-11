namespace _01._Sorts
{
    using System;
    using System.Collections.Generic;

    public static class Sorts<T> where T : IComparable
    {
        private static IList<T> _collection;
        private static T[] _tempCollection;

        public static void InPlaceMergeSort(IList<T> collection)
        {
            InPlaceMergeSort(collection, 0, collection.Count - 1);
        }

        private static void InPlaceMergeSort(IList<T> collection, int l, int r)
        {
            if (l < r)
            {

                // Same as (l + r) / 2, but avoids overflow 
                // for large l and r 
                var m = l + (r - l) / 2;

                // Sort first and second halves 
                InPlaceMergeSort(collection, l, m);
                InPlaceMergeSort(collection, m + 1, r);

                Merge(collection, l, m, r);
            }
        }

        static void Merge(IList<T> collection, int start, int mid, int end)
        {
            int start2 = mid + 1;

            // If the direct Merge is already sorted 
            if (Less(collection[start2], collection[mid]))
            {
                return;
            }

            // Two pointers to maintain start 
            // of both arrays to Merge 
            while (start <= mid && start2 <= end)
            {

                // If element 1 is in right place 
                if (Less(collection[start2], collection[start]))
                {
                    start++;
                }
                else
                {
                    var value = collection[start2];
                    var index = start2;

                    // Shift all the elements between element 1 
                    // element 2, right by 1. 
                    while (index != start)
                    {
                        collection[index] = collection[index - 1];
                        index--;
                    }

                    collection[start] = value;

                    // Update all the pointers 
                    start++;
                    mid++;
                    start2++;
                }
            }
        }

        public static void HeapSort(IList<T> collection)
        {
            //Worst Case: n*log(n), Average Case: n*log(n), Best Case: n*log(n)
            var length = collection.Count;
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(collection, length, i);
            }
            for (int i = length - 1; i >= 0; i--)
            {
                var temp = collection[0];
                collection[0] = collection[i];
                collection[i] = temp;
                Heapify(collection, i, 0);
            }
        }

        private static void Heapify(IList<T> collection, int length, int i)
        {
            var largest = i;
            var left = 2 * i + 1;
            var right = 2 * i + 2;

            if (left < length && Less(collection[left], collection[largest]))
            {
                largest = left;
            }

            if (right < length && Less(collection[right], collection[largest]))
            {
                largest = right;
            }

            if (largest != i)
            {
                var swap = collection[i];
                collection[i] = collection[largest];
                collection[largest] = swap;
                Heapify(collection, length, largest);
            }
        }

        public static void QuickSort(IList<T> collection)
        {
            //Worst Case: n^2, Average Case: n*log(n), Best Case: n*log(n)
            _collection = collection;
            QuickSort(0, _collection.Count - 1);
        }

        private static void QuickSort(int from, int to)
        {
            if (from >= to)
            {
                return;
            }

            var value = _collection[to];
            var counter = from;

            for (var i = from; i < to; i++)
            {
                if (Less(value, _collection[i]))
                {
                    Swap(_collection, i, counter);
                    counter++;
                }
            }

            Swap(_collection, counter, to);

            QuickSort(from, counter - 1);
            QuickSort(counter + 1, to);
        }

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
