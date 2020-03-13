namespace _02._Iterative_Combinations_without_Repetition
{
    using System;
    using System.Collections.Generic;

    public static class GenerateCombinationsIteratively
    {

        static void Main()
        {
            Console.Write("n = ");
            var n = int.Parse(Console.ReadLine());

            Console.Write("k = ");
            var k = int.Parse(Console.ReadLine());

            foreach (var combo in Combinations(k, n))
            {
                Console.WriteLine(string.Join(", ", combo));
            }
        }

        private static IEnumerable<int[]> Combinations(int k, int n)
        {
            var result = new int[k];
            var stack = new Stack<int>();
            stack.Push(1);

            while (stack.Count > 0)
            {
                var index = stack.Count - 1;
                var value = stack.Pop();

                while (value <= n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index == k)
                    {
                        yield return result;
                        break;
                    }
                }
            }
        }
    }
}
