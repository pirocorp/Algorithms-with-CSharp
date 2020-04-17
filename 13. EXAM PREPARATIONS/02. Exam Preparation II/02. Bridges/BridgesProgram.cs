namespace _02._Bridges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BridgesProgram
    {
        public static void Main()
        {
            var sequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var bridges = new List<Bridge>();

            for (var from = 0; from < sequence.Length; from++)
            {
                for (var to = from + 1; to < sequence.Length; to++)
                {
                    if (sequence[from] == sequence[to])
                    {
                        var bridge = new Bridge(from, to);

                        bridges.Add(bridge);
                        break;
                    }
                }
            }

            bridges = bridges.OrderBy(x => x.To)
                .ThenBy(x => x.Length)
                .ToList();

            var result = Enumerable.Repeat("X", sequence.Length).ToArray();
            var count = 0;

            foreach (var bridge in bridges)
            {
                var validBridge = true;

                for (var i = bridge.From + 1; i <= bridge.To; i++)
                {
                    if (result[i] != "X")
                    {
                        validBridge = false;
                        break;
                    }
                }

                if (validBridge)
                {
                    count++;
                    result[bridge.From] = sequence[bridge.From].ToString();
                    result[bridge.To] = sequence[bridge.To].ToString();
                }
            }


            var countResult = count == 0 ? "No" : count.ToString();
            var message = count == 1 ? "bridge found" : "bridges found";
            Console.WriteLine($"{countResult} {message}");
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
