namespace _01._Sorting
{
    using System;

    public static class MergeSort<T> where T : IComparable
    {
        private static T[] _aux;

        public static void Sort(T[] arr)
        {
            _aux = new T[arr.Length];
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(T[] arr, int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }

            var mid = (hi + lo) / 2;

            Sort(arr, lo, mid);
            Sort(arr, mid + 1, hi);

            Merge(arr, lo, mid, hi);
        }

        private static void Merge(T[] arr, int lo, int mid, int hi)
        {
            if (IsLess(arr[mid], arr[mid + 1]))
            {
                return;
            }

            for (var index = lo; index < hi + 1; index++)
            {
                _aux[index] = arr[index];
            }

            var i = lo;
            var j = mid + 1;

            for (var k = lo; k <= hi; k++)
            {
                if (i > mid)
                {
                    arr[k] = _aux[j++];
                }
                else if(j > hi)
                {
                    arr[k] = _aux[i++];
                }
                else if (IsLess(_aux[i], _aux[j]))
                {
                    arr[k] = _aux[i++];
                }
                else
                {
                    arr[k] = _aux[j++];
                }
            }
        }

        private static bool IsLess(IComparable first, IComparable second)
        {
            return first.CompareTo(second) < 0;
        }
    }
}
