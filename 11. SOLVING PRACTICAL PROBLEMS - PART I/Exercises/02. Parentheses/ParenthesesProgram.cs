namespace _02._Parentheses
{
    using System;

    public static class ParenthesesProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            PrintParentheses(n, 0, 0, string.Empty);
        }

        private static void PrintParentheses(int count, int openedCount, int closedCount, string result)
        {
            if (openedCount == count && closedCount == count)
            {
                Console.WriteLine(result);
                return;
            }

            if (openedCount == closedCount && openedCount < count)
            {
                PrintParentheses(count, openedCount + 1, closedCount, result + "(");
            }
            else
            {
                if (openedCount < count)
                {
                    PrintParentheses(count, openedCount + 1, closedCount, result + "(");
                }

                if (closedCount < openedCount)
                {
                    PrintParentheses(count, openedCount, closedCount + 1, result + ")");
                }
            }
        }
    }
}
