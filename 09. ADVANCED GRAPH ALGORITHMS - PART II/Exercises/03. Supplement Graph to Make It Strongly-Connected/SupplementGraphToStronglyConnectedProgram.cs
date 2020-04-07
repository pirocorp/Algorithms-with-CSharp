namespace _03._Supplement_Graph_to_Make_It_Strongly_Connected
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SupplementGraphToStronglyConnectedProgram
    {
        private static List<int>[] _graph;

        private static bool[] _visited;
        private static Stack<int> _topologicalOrder;
        private static List<int>[] _reversedGraph;
        private static List<List<int>> _stronglyConnectedComponents;

        private static List<int>[] _directedAcyclicGraph;

        private static void ReadInput()
        {
            var nodes = int.Parse(Console.ReadLine().Split(' ')[1]);
            var edges = int.Parse(Console.ReadLine().Split(' ')[1]);

            _graph = new List<int>[nodes];

            for (var i = 0; i < _graph.Length; i++)
            {
                _graph[i] = new List<int>();
            }

            for (var i = 0; i < edges; i++)
            {
                var edgeTokens = Console.ReadLine()
                    .Split(new []{" -> "}, StringSplitOptions.RemoveEmptyEntries);
                var parent = int.Parse(edgeTokens[0]);
                var child = int.Parse(edgeTokens[1]);

                _graph[parent].Add(child);
            }
        }

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

            //Build Topological sort of Strongly connected Components
            for (var node = 0; node < _graph.Length; node++)
            {
                if (!_visited[node])
                {
                    Dfs(node);
                }
            }

            _stronglyConnectedComponents = new List<List<int>>();
            _visited = new bool[_graph.Length];

            //Map each strongly connected component
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

        private static void BuildDagGraph()
        {
            _directedAcyclicGraph = new List<int>[_stronglyConnectedComponents.Count];

            for (var i = 0; i < _directedAcyclicGraph.Length; i++)
            {
                _directedAcyclicGraph[i] = new List<int>();
            }

            for (var dagNodeSource = 0; dagNodeSource < _stronglyConnectedComponents.Count; dagNodeSource++)
            {
                for (var dagNodeDestination = 0; dagNodeDestination < _stronglyConnectedComponents.Count; dagNodeDestination++)
                {
                    if (dagNodeSource == dagNodeDestination)
                    {
                        continue;
                    }

                    foreach (var sourceNode in _stronglyConnectedComponents[dagNodeSource])
                    {
                        foreach (var destinationNode in _stronglyConnectedComponents[dagNodeDestination])
                        {
                            if (_graph[sourceNode].Contains(destinationNode))
                            {
                                if (!_directedAcyclicGraph[dagNodeSource].Contains(dagNodeDestination))
                                {
                                    _directedAcyclicGraph[dagNodeSource].Add(dagNodeDestination);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CalculateNeededEdges()
        {
            var withoutInEdgesSet = new HashSet<int>(Enumerable.Range(0, _directedAcyclicGraph.Length));
            withoutInEdgesSet.ExceptWith(_directedAcyclicGraph.SelectMany(x => x).Distinct());

            var withoutOutEdges = new List<int>();

            for (var node = 0; node < _directedAcyclicGraph.Length; node++)
            {
                if (_directedAcyclicGraph[node].Count == 0)
                {
                    withoutOutEdges.Add(node);
                }
            }

            var newEdges = new List<string>();
            var withoutInEdges = withoutInEdgesSet.ToArray();

            if (withoutOutEdges.Count > withoutInEdgesSet.Count)
            {
                for (var i = 0; i < withoutOutEdges.Count; i++)
                {
                    var source = withoutOutEdges[i];
                    int dest;

                    if (i < withoutInEdgesSet.Count)
                    {
                        dest = withoutInEdges[i];
                    }
                    else
                    {
                        var withOutAndIn = new HashSet<int>(Enumerable.Range(0, _directedAcyclicGraph.Length));
                        withOutAndIn.ExceptWith(withoutOutEdges);
                        withOutAndIn.ExceptWith(withoutInEdgesSet);

                        dest = withOutAndIn.First();
                    }

                    var newEdge =
                        $"{_stronglyConnectedComponents[source].First()} -> {_stronglyConnectedComponents[dest].First()}";
                    newEdges.Add(newEdge);

                    _directedAcyclicGraph[source].Add(dest);
                }
            }
            else
            {
                for (var i = 0; i < withoutInEdges.Length; i++)
                {
                    var dest = withoutInEdges[i];
                    int source;

                    if (i < withoutOutEdges.Count)
                    {
                        source = withoutOutEdges[i];
                    }
                    else
                    {
                        var withOutAndIn = new HashSet<int>(Enumerable.Range(0, _directedAcyclicGraph.Length));
                        withOutAndIn.ExceptWith(withoutOutEdges);
                        withOutAndIn.ExceptWith(withoutInEdgesSet);

                        source = withOutAndIn.First();
                    }

                    var newEdge =
                        $"{_stronglyConnectedComponents[source].First()} -> {_stronglyConnectedComponents[dest].First()}";
                    newEdges.Add(newEdge);

                    _directedAcyclicGraph[source].Add(dest);
                }
            }

            var neededEdgesCount = Math.Max(withoutOutEdges.Count, withoutInEdgesSet.Count);
            Console.WriteLine($"New edges needed: {neededEdgesCount}");

            foreach (var edge in newEdges)
            {
                Console.WriteLine(edge);
            }
        }

        public static void Main()
        {
            ReadInput();
            FindStronglyConnectedComponents();
            BuildDagGraph();
            CalculateNeededEdges();
        }
    }
}
