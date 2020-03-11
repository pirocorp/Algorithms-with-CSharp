namespace _02._QuickSort
{
    using System;

    public static class QuickSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable
        {
            Sort<T>(arr, 0, arr.Length - 1);
        }

        private static void Sort<T>(T[] arr, int lo, int hi) where T : IComparable
        {
            if (lo >= hi)
            {
                return;
            }

            var p = Partition(arr, lo, hi);
            Sort(arr, lo, p - 1);
            Sort(arr, p + 1, hi);
        }

        private static int Partition<T>(T[] arr, in int lo, in int hi) where T : IComparable
        {
            if (lo >= hi)
            {
                return lo;
            }

            var i = lo;
            var j = hi + 1;

            while (true)
            {
                while (IsLess(arr[++i], arr[lo]))
                {
                    if (i == hi)
                    {
                        break;
                    }
                }

                while (IsLess(arr[lo], arr[--j]))
                {
                    if (j == lo)
                    {
                        break;
                    }
                }

                if (i >= j)
                {
                    break;
                }

                Swap(arr, i, j);
            }

            Swap(arr, lo, j);
            return j;
        }

        private static bool IsLess(IComparable first, IComparable second)
        {
            return first.CompareTo(second) < 0;
        }

        private static void Swap<T>(T[] collection, int from, int to)
        {
            var swap = collection[from];
            collection[from] = collection[to];
            collection[to] = swap;
        }
    }
}
