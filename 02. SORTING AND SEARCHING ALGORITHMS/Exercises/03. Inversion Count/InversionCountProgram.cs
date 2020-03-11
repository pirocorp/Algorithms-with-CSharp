namespace _03._Inversion_Count
{
    using System;
    using System.Linq;

    public static class InversionCountProgram
    {
        public static void Main()
        {
            var inputArr = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var inversions = MergeSort(inputArr);
            Console.WriteLine(inversions);
        }

        private static int MergeSort(int[] arr)
        {
            var size = arr.Length;
            var temp = new int[size];

            return MergeSort(arr, temp, 0, size - 1);
        }

        private static int MergeSort(int[] arr, int[] temp, int left, int right)
        {
            var invertions = 0;

            if (right > left)
            {
                var mid = (right + left) / 2;
                invertions += MergeSort(arr, temp, left, mid);
                invertions += MergeSort(arr, temp, mid + 1, right);

                invertions += Merge(arr, temp, left, mid, right);
            }

            return invertions;
        }

        private static int Merge(int[] arr, int[] temp, int left, int mid, int right)
        {
            var invertions = 0;

            var i = left;
            var j = mid + 1;
            var k = left;

            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j])
                {
                    temp[k++] = arr[i++];
                }
                else
                {
                    temp[k++] = arr[j++];
                    invertions = invertions + (mid + 1 - i);
                }
            }

            while (i <= mid)
            {
                temp[k++] = arr[i++];
            }

            while (j <= right)
            {
                temp[k++] = arr[j++];
            }

            for (i = left; i <= right; i++)
            {
                arr[i] = temp[i];
            }

            return invertions;
        }
    }
}
