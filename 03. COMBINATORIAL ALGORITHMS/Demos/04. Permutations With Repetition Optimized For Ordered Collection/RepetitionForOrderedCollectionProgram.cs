namespace _04._Permutations_With_Repetition_Optimized_For_Ordered_Collection
{
    using System;

    public static class RepetitionForOrderedCollectionProgram
    {
        public static void Main()
        {
            var arr = new int[] { 3, 5, 1, 5, 5 };
            Array.Sort(arr);
            PermuteRep(arr, 0, arr.Length - 1);
        }

        private static void PermuteRep(int[] arr, int start, int end)
        {
            Print(arr);

            for (var left = end - 1; left >= start; left--)
            {
                for (var right = left + 1; right <= end; right++)
                {
                    if (arr[left] != arr[right])
                    {
                        Swap(ref arr[left], ref arr[right]);
                        PermuteRep(arr, left + 1, end);
                    }
                }

                var firstElement = arr[left];

                for (var i = left; i < end; i++)
                {
                    arr[i] = arr[i + 1];
                }

                arr[end] = firstElement;
            }
        }

        private static void Swap(ref int left, ref int right)
        {
            var swap = left;
            left = right;
            right = swap;
        }

        private static void Print(int[] arr)
        {
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
