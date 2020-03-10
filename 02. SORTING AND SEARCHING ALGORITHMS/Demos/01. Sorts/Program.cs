namespace _01._Sorts
{
    using System;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var collection = new List<int>()
            {
                3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48
            };

            //Sorts<int>.SelectionSort(collection);
            //Sorts<int>.BubbleSort(collection);

            Console.WriteLine(string.Join(", ", collection));
        }
    }
}
