namespace _01._Medenka
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

        private static int GetNuts(string medenka)
        {
            var nuts = 0;

            for (var i = 0; i < medenka.Length; i++)
            {
                if (medenka[i] == '1')
                {
                    nuts++;
                }
            }

            return nuts;
        }

        private static void Print(string medenka, List<int> indexes)
        {
            var sb = new StringBuilder();

            for (int i = medenka.Length - 1; i >= 0; i--)
            {
                if (indexes.Contains(i))
                {
                    sb.Append('|');
                }

                sb.Append(medenka[i]);
            }

            var result = new string(sb.ToString().Reverse().ToArray());
            Console.WriteLine(result);
        }

        private static void GenerateCuts(int start, int cuts, int nuts,
            string medenka, List<int> indexes)
        {
            if (cuts == nuts - 1)
            {
                Print(medenka, indexes);
            }
            else
            {
                var end = medenka.IndexOf('1', start + 1);

                for (var i = start; i < end; i++)
                {
                    indexes.Add(i);

                    GenerateCuts(end, cuts + 1, nuts, medenka, indexes);
                    indexes.RemoveAt(indexes.Count - 1);
                }
            }
        }

        private static void OtherSolution()
        {
            var medenka = string.Join("", Console.ReadLine().Split(' '));

            var result = new List<int>();

            var start = medenka.IndexOf('1');
            var nuts = GetNuts(medenka);

            GenerateCuts(start, 0, nuts, medenka, result);
        }

        public static void Main()
        {
            //MySolution();

            OtherSolution();
        }
    }
}
