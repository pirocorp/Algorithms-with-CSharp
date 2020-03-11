namespace _03._Binary_Search
{
    using System;

    public static class BinarySearchProgram
    {
        public static void Main()
        {
            var arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 105, 106, 200};
            var index = BinarySearch.IndexOf(arr, 105);

            Console.WriteLine(arr[index]);
        }
    }
}
