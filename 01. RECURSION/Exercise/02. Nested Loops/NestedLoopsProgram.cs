namespace _02._Nested_Loops
{
    using System;

    public static class NestedLoopsProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var vector = new int[n];
            Generate(0, vector, n);
        }

        private static void Generate(int index, int[] vector, int n)
        {
            if (index >= vector.Length)
            {
                Print(vector);
            }
            else
            {
                for (var i = 1; i <= n; i++)
                {
                    vector[index] = i;
                    Generate(index + 1, vector, n);
                }
            }
        }

        private static void Print(int[] vector)
        {
            Console.WriteLine(string.Join(" ", vector));
        }
    }
}
