namespace _04._Needles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class NeedlesProgram
    {
        public static void Main()
        {
            var input = ReadArrayOfNumbersFromConsole();
            var elementsCount = input[0];
            var needlesCount = input[1];
            var indexes = new List<int>();

            var inputArr = ReadArrayOfNumbersFromConsole();
            var needles = ReadArrayOfNumbersFromConsole();

            foreach (var needle in needles)
            {
                var found = false;

                for (var index = 0; index < elementsCount; index++)
                {
                    if (inputArr[index] >= needle)
                    {
                        found = true;
                        indexes.Add(GetNonZeroIndex(inputArr, index - 1));
                        break;
                    }
                }

                if (!found)
                {
                    indexes.Add(GetNonZeroIndex(inputArr, elementsCount - 1));
                }
            }

            Console.WriteLine(string.Join(" ", indexes));
        }

        private static int GetNonZeroIndex(int[] inputArr, int index)
        {
            for (var i = index; i >= 0; i--)
            {
                if (inputArr[i] != 0)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        private static int[] ReadArrayOfNumbersFromConsole()
        {
            return Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }
}
