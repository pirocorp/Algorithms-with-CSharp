namespace _02._Conditional_Expression_Resolver;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var input = (Console.ReadLine() ?? string.Empty);

        var result = Resolve(input);

        Console.WriteLine(result);
    }

    private static string Resolve(string input)
    {
        var stack = new Stack<string>();

        var tokens = input
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();

        for (var i = tokens.Length - 1; i >= 0; i--)
        {
            var current = tokens[i];

            if (stack.Count != 0 && stack.Peek() == "?")
            {
                stack.Pop();

                var first = stack.Pop();

                stack.Pop();

                var second = stack.Pop();

                stack.Push(current == "t" ? first : second);
            }
            else
            {
                stack.Push(current);
            }
        }

        return stack.Peek();
    }
}
