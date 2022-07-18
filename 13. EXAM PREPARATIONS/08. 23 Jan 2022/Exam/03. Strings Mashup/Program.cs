namespace _03._Strings_Mashup;

using System;
using System.Collections.Generic;

public static class Program
{
    private static char[] input;

    private static readonly List<string> results;

    static Program()
    {
        input = Array.Empty<char>();
        results = new List<string>();
    }

    public static void Main()
    {
        input = (Console.ReadLine() ?? string.Empty)
            .ToLower()
            .ToCharArray();

        GenerateStrings(string.Empty, 0);

        Console.WriteLine(string.Join(Environment.NewLine, results));
    }

    private static void GenerateStrings(string str, int index)
    {
        if (index < input.Length)
        {
            var newStr = str + input[index];
            GenerateStrings(newStr, index + 1);

            if (char.IsLetter(input[index]))
            {
                newStr = str + input[index].ToString().ToUpper();
                GenerateStrings(newStr, index + 1);
            }
        }
        else
        {
            results.Add(str);
        }
    }
}
