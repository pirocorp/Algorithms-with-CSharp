namespace _03._Binary_Search
{
    public static class BinarySearch
    {
        public static int IndexOf(int[] arr, int key)
        {
            var lo = 0;
            var hi = arr.Length - 1;

            while (lo <= hi)
            {
                var mid = lo + (hi - lo) / 2;
                if (key < arr[mid])
                {
                    hi = mid - 1;
                }
                else if (key > arr[mid])
                {
                    lo = mid + 1;
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }
    }
}
