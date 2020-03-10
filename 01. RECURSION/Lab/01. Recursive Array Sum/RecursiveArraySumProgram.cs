namespace _01._Recursive_Array_Sum
{
    using System;
    using System.Linq;

    public static class RecursiveArraySumProgram
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = Sum(numbers, 0);
            Console.WriteLine(result);
        }

        public static int Sum(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return 0;
            }

            var currentSum = arr[index] + Sum(arr, index + 1);
            return currentSum;
        }
    }
}
