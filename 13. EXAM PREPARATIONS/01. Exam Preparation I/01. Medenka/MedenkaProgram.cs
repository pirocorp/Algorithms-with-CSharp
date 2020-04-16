namespace _01._Medenka
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MedenkaProgram
    {
        private static void Generate(List<int>[] locations, int index, List<char> medenka)
        {
            if (index == locations.Length)
            {
                Console.WriteLine(new string(medenka.ToArray()));
                return;
            }

            for (var i = 0; i < locations[index].Count; i++)
            {
                var pipeIndex = locations[index][i];
                medenka.Insert(pipeIndex, '|');
                Generate(locations, index + 1, medenka);
                medenka.RemoveAt(pipeIndex);
            }
        }

        private static void MySolution()
        {
            var medenka = Console.ReadLine()
                .Split(' ')
                .Select(x => x[0])
                .ToList();

            var pipeCount = medenka.Count(x => x == '1') - 1;

            var possiblePipeLocations = new List<int>[pipeCount];

            for (var i = medenka.Count - 1; i >= 0; i--)
            {
                if (medenka[i] == '1')
                {
                    if (pipeCount - 1 < 0)
                    {
                        break;
                    }

                    possiblePipeLocations[--pipeCount] = new List<int>();
                }

                possiblePipeLocations[pipeCount].Add(i);
            }

            possiblePipeLocations = possiblePipeLocations.Reverse().ToArray();

            Generate(possiblePipeLocations, 0, medenka);
        }

        public static void Main()
        {
            MySolution();

            var medenka = Console.ReadLine();

            var result = new List<string>();

            var start = medenka.IndexOf('1');
            GenerateCuts(start, medenka, result);
        }

        private static void GenerateCuts(int start, string medenka, List<string> result)
        {
            if (start >= medenka.Length)
            {
                //Print
            }
            else
            {
                var end = medenka.LastIndexOf('1', start + 1);
            }
        }
    }
}
