namespace _07._Variations_With_Repetitions_Iterative
{
    using System;

    public static class VariationsWithRepetitionsIterativeProgram
    {
        public static void Main()
        {
            var n = 5;
            var k = 3;

            var arr = new int[k];
            var cary = 0;

            while (cary == 0)
            {
                Print(arr);

                arr[k - 1]++;

                for (var i = k-1; i >= 0; i--)
                {
                     arr[i] += cary;
                     cary = arr[i] / n;
                     arr[i] %= n;
                }
            }
        }

        private static void Print(int[] arr)
        {
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
