namespace _03._Combinations_with_Repetition
{
    using System;

    public static class CombinationsWithRepetitionProgram
    {
        public static void Main()
        {
            var set = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            var vector = new int[k];

            GetCombos(set, vector, 0, 1);
        }

        private static void GetCombos(int set, int[] vector, int index, int border)
        {
            if (index >= vector.Length)
            {
                Print(vector);
            }
            else
            {
                for (var i = border; i <= set; i++)
                {
                    vector[index] = i;
                    GetCombos(set, vector, index + 1, i);
                }
            }
        }

        private static void Print(int[] vector)
        {
            Console.WriteLine(string.Join(" ", vector));
        }
    }
}
