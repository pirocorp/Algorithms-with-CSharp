namespace Sum_To_13
{
    using System;
    using System.Linq;

    public static class SumToProgram
    {
        private const int TARGET = 13;

        private static int ChooseNumber(int number, int index)
        {
            if (index % 2 == 0)
            {
                return number;
            }
            else
            {
                return number * -1;
            }
        }

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = "No";

            for (int i = 0; i < 2; i++)
            {
                var n1 = ChooseNumber(numbers[0], i);

                for (int j = 0; j < 2; j++)
                {
                    var n2 = ChooseNumber(numbers[1], j);

                    for (int k = 0; k < 2; k++)
                    {
                        var n3 = ChooseNumber(numbers[2], k);

                        if (n1 + n2 + n3 == TARGET)
                        {
                            result = "Yes";
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
