namespace _02._QuickSort
{
    using System;
    using System.Linq;

    public static class QuickSortProgram
    {
        public static void Main()
        {
            var inputArr = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            QuickSort.Sort(inputArr);
            Console.WriteLine(string.Join(", ", inputArr));
        }
    }
}
