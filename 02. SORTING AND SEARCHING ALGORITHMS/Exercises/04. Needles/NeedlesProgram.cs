namespace _04._Needles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class NeedlesProgram
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var indexes = new List<int>();

            var inputArr = ReadArrayOfNumbersFromConsole();
            var needles = ReadArrayOfNumbersFromConsole();

            for (var i = 0; i < needles.Length; i++)
            {
                var currentNeedle = needles[i];

                var index = 0;
                var needleIndex = -1;

                while (index < inputArr.Length && inputArr[index] < currentNeedle)
                {
                    if (inputArr[index] != 0)
                    {
                        needleIndex = index;
                    }

                    index++;
                }

                indexes.Add(needleIndex + 1);
            }

            Console.WriteLine(string.Join(" ", indexes));
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
