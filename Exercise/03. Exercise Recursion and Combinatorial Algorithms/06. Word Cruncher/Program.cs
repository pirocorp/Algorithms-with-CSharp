namespace _06._Word_Cruncher;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static readonly Dictionary<int, List<string>> wordsByIndex = new Dictionary<int, List<string>>();
    private static readonly Dictionary<string, int> wordsCount = new Dictionary<string, int>();

    private static string target = string.Empty;
    private static readonly LinkedList<string> solution = new LinkedList<string>();

    public static void Main()
    {
        ReadInput();
        Crunch(0);
    }

    private static void ReadInput()
    {
        var input = (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();

        target = (Console.ReadLine() ?? string.Empty).Trim();

        foreach (var word in input)
        {
            var index = target.IndexOf(word, StringComparison.InvariantCulture);

            if (index == -1)
            {
                continue;
            }

            if (wordsCount.ContainsKey(word))
            {
                wordsCount[word] += 1;
                continue;
            }

            wordsCount[word] = 1;

            while (index != -1)
            {
                if (!wordsByIndex.ContainsKey(index))
                {
                    wordsByIndex.Add(index, new List<string>());
                }

                wordsByIndex[index].Add(word);

                index = target.IndexOf(word, index + 1, StringComparison.InvariantCulture);
            }
        }
    }

    private static void Crunch(int index)
    {
        if (index >= target.Length)
        {
            Console.WriteLine(string.Join(" ", solution));

            return;
        }

        if (!wordsByIndex.ContainsKey(index))
        {
            return;
        }

        foreach (var word in wordsByIndex[index])
        {
            if (wordsCount[word] == 0)
            {
                continue;
            }

            wordsCount[word] -= 1;
            solution.AddLast(word);

            Crunch(index + word.Length);

            wordsCount[word] += 1;
            solution.RemoveLast();
        }
    }
}
