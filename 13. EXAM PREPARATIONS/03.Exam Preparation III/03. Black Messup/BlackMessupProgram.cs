namespace _03._Black_Messup
{
    using System;
    using System.Collections.Generic;

    public static class BlackMessupProgram
    {
        private static Dictionary<string, Atom> _atoms;
        private static Dictionary<string, SortedSet<string>> _graph;

        private static void ReadInput()
        {
            _atoms = new Dictionary<string, Atom>();
            _graph = new Dictionary<string, SortedSet<string>>();

            var atomsCount = int.Parse(Console.ReadLine());
            var atomsConnections = int.Parse(Console.ReadLine());

            for (var i = 0; i < atomsCount; i++)
            {
                var atomArgs = Console.ReadLine()
                    .Split();

                var name = atomArgs[0];
                var mass = int.Parse(atomArgs[1]);
                var decay = int.Parse(atomArgs[2]);

                var atom = new Atom(name, mass, decay);
                _atoms[atom.Name] = atom;
                _graph[atom.Name] = new SortedSet<string>();
            }

            for (var i = 0; i < atomsConnections; i++)
            {
                var connectionArgs = Console.ReadLine()
                    .Split();

                var firstAtom = connectionArgs[0];
                var secondAtom = connectionArgs[1];

                _graph[firstAtom].Add(secondAtom);
                _graph[secondAtom].Add(firstAtom);
            }
        }

        private static void Dfs(string node, string parent, HashSet<string> visited, List<SortedSet<Atom>> molecules, int index)
        {
            visited.Add(node);
            molecules[index].Add(_atoms[node]);

            foreach (var child in _graph[node])
            {
                if (!visited.Contains(child)
                    && child != parent)
                {
                    Dfs(child, node, visited, molecules, index);
                }
            }
        }

        private static List<SortedSet<Atom>> FindConnectedComponents()
        {
            var molecules = new List<SortedSet<Atom>>();

            var visited = new HashSet<string>();
            var index = 0;

            foreach (var node in _graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    molecules.Add(new SortedSet<Atom>());
                    Dfs(node, node, visited, molecules, index);
                    index++;
                }
            }

            return molecules;
        }

        private static int GetScore(SortedSet<Atom> molecule)
        {
            var maxDecay = 1;
            var score = 0;
            var count = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    maxDecay = atom.Decay;
                    score += atom.Mass;
                    count++;
                }
                else if(maxDecay > count)
                {
                    score += atom.Mass;
                    count++;
                }
            }

            return score;
        }

        private static int FindBestMoleculeValue(List<SortedSet<Atom>> molecules)
        {
            var max = 0;

            foreach (var molecule in molecules)
            {
                var score = GetScore(molecule);

                if (score > max)
                {
                    max = score;
                }
            }

            return max;
        }

        public static void Main()
        {
            ReadInput();

            var molecules = FindConnectedComponents();

            Console.WriteLine(FindBestMoleculeValue(molecules));
        }
    }
}
