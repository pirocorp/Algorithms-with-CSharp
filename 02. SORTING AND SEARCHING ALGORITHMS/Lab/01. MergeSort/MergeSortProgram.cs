namespace _01._MergeSort
{
    using System;
    using System.Linq;

    public static class MergeSortProgram
    {
        public static void Main()
        {
            var inputArr = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            MergeSort<int>.Sort(inputArr);
            Console.WriteLine(string.Join(", ", inputArr));
        }
    }
}
