namespace _01._Sorts
{
    using System;
    using System.Collections.Generic;

    public static class SortsDemoProgram
    {
        public static void Main()
        {
            var collection = new List<int>()
            {
                3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48
            };

            //Sorts<int>.SelectionSort(collection);
            //Sorts<int>.BubbleSort(collection);
            //Sorts<int>.InsertionSort(collection);
            //Sorts<int>.MergeSort(collection);
            //Sorts<int>.QuickSort(collection);
            //Sorts<int>.HeapSort(collection);

            Console.WriteLine(string.Join(", ", collection));
        }
    }
}
