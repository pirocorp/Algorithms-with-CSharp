namespace _01._Reverse_Array
{
    using System;
    using System.Linq;

    public static class ReverseArrayProgram
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            ReverseArray(0, numbers);
        }

        private static void ReverseArray(int index, int[] numbers)
        {
            if (index == numbers.Length)
            {
                return;
            }

            ReverseArray(index + 1, numbers);
            Console.Write($"{numbers[index]} ");
        }
    }
}
