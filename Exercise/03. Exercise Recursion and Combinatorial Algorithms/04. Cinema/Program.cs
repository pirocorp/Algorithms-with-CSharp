namespace _04._Cinema;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    private static List<string> friendsList = new List<string>();
    private static readonly Dictionary<string, int> predefinedPlaces = new Dictionary<string, int>();

    private static string[] permutation = Array.Empty<string>();
    private static bool[] used = Array.Empty<bool>();

    private static readonly List<string[]> permutations = new List<string[]>();

    public static void Main()
    {
        ReadInput();
        GeneratePossibleOrders(0);
        GenerateOutput();
    }

    private static void ReadInput()
    {
        var friends = (Console.ReadLine() ?? string.Empty)
            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToHashSet();

        string input;

        while ((input = Console.ReadLine() ?? string.Empty) != "generate")
        {
            var tokens = input.Split(" - ");

            var name = tokens[0];
            var place = int.Parse(tokens[1]);

            predefinedPlaces.Add(name, place);
        }

        foreach (var friend in predefinedPlaces.Keys)
        {
            friends.Remove(friend);
        }

        friendsList = friends.ToList();

        permutation = new string[friendsList.Count];
        used = new bool[friendsList.Count];
    }

    private static void GeneratePossibleOrders(int index)
    {
        if (index >= permutation.Length)
        {
            permutations.Add(permutation.ToArray());
        }
        else
        {
            for (var i = 0; i < friendsList.Count; i++)
            {
                if (used[i])
                {
                    continue;
                }

                used[i] = true;
                permutation[index] = friendsList[i];
                GeneratePossibleOrders(index + 1);
                used[i] = false;
            }
        }
    }

    private static void GenerateOutput()
    {
        var count = friendsList.Count + predefinedPlaces.Count;

        var order = new string[count];

        foreach (var (friend, place) in predefinedPlaces)
        {
            order[place - 1] = friend;
        }


        foreach (var friendsOrder in permutations)
        {
            var currentOrder = order.ToArray();

            var index = 0;

            for (var i = 0; i < currentOrder.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(currentOrder[i]))
                {
                    currentOrder[i] = friendsOrder[index++];
                }
            }

            Console.WriteLine(string.Join(" ", currentOrder));
        }
    }
}
