namespace _04._Road_Reconstruction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RoadReconstructionProgram
    {
        private static HashSet<int>[] _graph;
        private static List<Edge> _listOfEdges;
        private static List<Edge> _result;

        private static List<Dictionary<int, HashSet<int>>> _connectedComponents;

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            _graph = new HashSet<int>[nodesCount];

            for (var i = 0; i < nodesCount; i++)
            {
                _graph[i] = new HashSet<int>();
            }

            _listOfEdges = new List<Edge>();
            _result = new List<Edge>();

            for (var i = 0; i < edgesCount; i++)
            {
                var edgeArgs = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeArgs[0];
                var to = edgeArgs[1];

                var edge = new Edge(from, to);
                var reverseEdge = new Edge(to, from);

                _graph[from].Add(to);
                _graph[to].Add(from);

                _listOfEdges.Add(edge);
            }
        }

        private static void Map(int node, List<int> currentComponent, bool[] visited)
        {
            if (!visited[node])
            {
                visited[node] = true;
                currentComponent.Add(node);
                var children = _graph[node];

                foreach (var child in children)
                {
                    Map(child, currentComponent, visited);
                }
            }
        }

        private static void FindConnectedComponents()
        {
            var visited = new bool[_graph.Length];
            var mappedComponents = new List<List<int>>();
            _connectedComponents = new List<Dictionary<int, HashSet<int>>>();

            for (var i = 0; i < _graph.Length; i++)
            {
                if (!visited[i])
                {
                    var currentComponent = new List<int>();
                    Map(i, currentComponent, visited);
                    mappedComponents.Add(currentComponent);
                }
            }

            for (var i = 0; i < mappedComponents.Count; i++)
            {
                var mappedComponent = mappedComponents[i];
                _connectedComponents.Add(new Dictionary<int, HashSet<int>>());

                foreach (var node in mappedComponent)
                {
                    _connectedComponents[i].Add(node, _graph[node]);
                }
            }
        }

        private static void Dfs(int node, bool[] visited)
        {
            if (!visited[node])
            {
                visited[node] = true;
                var children = _graph[node];

                foreach (var child in children)
                {
                    Dfs(child, visited);
                }
            }
        }

        private static void FindImportantEdges()
        {
            foreach (var edge in _listOfEdges)
            {
                _graph[edge.From].Remove(edge.To);
                _graph[edge.To].Remove(edge.From);

                var visited = new bool[_graph.Length];
                Dfs(edge.From, visited);

                var currentComponent = _connectedComponents
                    .First(x => x.ContainsKey(edge.From));

                var isImportant = false;

                for (var i = 0; i < visited.Length; i++)
                {
                    if (!visited[i] && currentComponent.ContainsKey(i))
                    {
                        isImportant = true;
                    }
                }

                if (isImportant)
                {
                    _result.Add(edge);
                }

                _graph[edge.From].Add(edge.To);
                _graph[edge.To].Add(edge.From);
            }
        }

        public static void Main()
        {
            ReadInput();

            FindConnectedComponents();

            FindImportantEdges();

            Console.WriteLine($"Important streets:");

            foreach (var edge in _result)
            {
                Console.WriteLine($"{edge.From} {edge.To}");
            }
        }
    }
}
