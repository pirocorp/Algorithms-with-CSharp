namespace _01.__Strongly_Connected_Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StronglyConnectedComponentsProgram
    {
        private static readonly List<int>[] _graph = new List<int>[]
        {
            new List<int>() {1, 11, 13}, // children of node 0
            new List<int>() {6},         // children of node 1
            new List<int>() {0},         // children of node 2
            new List<int>() {4},         // children of node 3
            new List<int>() {3, 6},      // children of node 4
            new List<int>() {13},        // children of node 5
            new List<int>() {0, 11},     // children of node 6
            new List<int>() {12},        // children of node 7
            new List<int>() {6, 11},     // children of node 8
            new List<int>() {0},         // children of node 9
            new List<int>() {4, 6, 10},  // children of node 10
            new List<int>() {},          // children of node 11
            new List<int>() {7},         // children of node 12
            new List<int>() {2, 9},      // children of node 13
        };
        private static bool[] _visited;
        private static Stack<int> _topologicalOrder;
        private static List<int>[] _reversedGraph;
        private static List<List<int>> _stronglyConnectedComponents;

        //Reversing graph makes swaps last and first in topological order
        //So first in topologicalOrder will be
        //last topological ordered in reversed graph
        private static void Dfs(int node)
        {
            if (!_visited[node])
            {
                _visited[node] = true;

                foreach (var child in _graph[node])
                {
                     Dfs(child);
                }

                _topologicalOrder.Push(node);
            }
        }

        private static void BuildReverseGraph()
        {
            _reversedGraph = new List<int>[_graph.Length];

            for (var node = 0; node < _reversedGraph.Length; node++)
            {
                _reversedGraph[node] = new List<int>();
            }

            for (var node = 0; node < _graph.Length; node++)
            {
                foreach (var child in _graph[node])
                {
                    _reversedGraph[child].Add(node);
                }
            }
        }

        private static void ReverseDfs(int node)
        {
            if (!_visited[node])
            {
                _visited[node] = true;
                _stronglyConnectedComponents.Last().Add(node);

                foreach (var child in _reversedGraph[node])
                {
                    ReverseDfs(child);
                }
            }
        }

        private static void FindStronglyConnectedComponents()
        {
            _topologicalOrder = new Stack<int>();
            _visited = new bool[_graph.Length];

            BuildReverseGraph();

            for (var node = 0; node < _graph.Length; node++)
            {
                if (!_visited[node])
                {
                    Dfs(node);
                }
            }

            _stronglyConnectedComponents = new List<List<int>>();
            _visited = new bool[_graph.Length];

            while (_topologicalOrder.Count > 0)
            {
                var node = _topologicalOrder.Pop();

                if (!_visited[node])
                {
                    _stronglyConnectedComponents.Add(new List<int>());
                    ReverseDfs(node);
                }
            }
        }

        public static void Main()
        {
            FindStronglyConnectedComponents();

            Console.WriteLine("Strongly Connected Components:");
            foreach (var component in _stronglyConnectedComponents
                .OrderBy(x => x.Count)
                .ThenBy(x => x.Sum()))
            {
                Console.WriteLine("{{{0}}}", string.Join(", ", component));
            }
        }
    }
}