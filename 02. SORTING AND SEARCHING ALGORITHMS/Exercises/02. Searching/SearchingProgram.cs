namespace _02._Searching
{
    using System;
    using System.Linq;

    public static class SearchingProgram
    {
        public static void Main()
        {
            var inputArr = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var key = int.Parse(Console.ReadLine());

            var index = BinarySearch.IndexOf(inputArr, key);
            Console.WriteLine(index);
        }
    }
}
