namespace _02._Permutations_with_Repetition;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static string[] elements = Array.Empty<string>();

    public static void Main(string[] args)
    {
        elements = Console.ReadLine()
            ?.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray() ?? Array.Empty<string>();

        Permute(0);
    }

    private static void Permute(int index)
    {
        if (index >= elements.Length)
        {
            Print();
        }
        else
        {
            Permute(index + 1);

            var swapped = new HashSet<string> { elements[index] };

            for (var i = index + 1; i < elements.Length; i++)
            {
                if (swapped.Contains(elements[i]))
                {
                    continue;
                }

                Swap(index, i);
                Permute(index + 1);
                Swap(index, i);
                swapped.Add(elements[i]);
            }
        }
    }

    private static void Swap(int i1, int i2)
        => (elements[i1], elements[i2]) = (elements[i2], elements[i1]);

    private static void Print()
        => Console.WriteLine(string.Join(" ", elements));
}

