namespace _03._Renewal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RenewalProgram
    {
        private static bool[,] _adjacencyMatrix;
        private static int[,] _buildMatrix;
        private static int[,] _destroyMatrix;

        private static List<Edge> _edgesBetweenConnectedComponents;

        private static Dictionary<string, Edge> _edgesToBeDestroyed;
        private static List<Edge> _edgesToBeBuild;

        private static void BuildEdgeMatrix(int size)
        {
            for (var row = 0; row < size; row++)
            {
                var inputArgs = Console.ReadLine()
                    .Select(x => x == '1' ? true : false)
                    .ToArray();

                for (var col = 0; col < inputArgs.Length; col++)
                {
                    _adjacencyMatrix[row, col] = inputArgs[col];
                }
            }
        }

        private static void BuildDataMatrix(int size, int[,] matrix)
        {
            for (var row = 0; row < size; row++)
            {
                var inputArgs = Console.ReadLine()
                    .Select(x => char.IsUpper(x) ? x - 'A' : x - 'a' + 26)
                    .ToArray();

                for (var col = 0; col < inputArgs.Length; col++)
                {
                    matrix[row, col] = inputArgs[col];
                }
            }
        }

        private static void ReadInput()
        {
            var size = int.Parse(Console.ReadLine());

            _adjacencyMatrix = new bool[size, size];
            _buildMatrix = new int[size, size];
            _destroyMatrix = new int[size, size];

            BuildEdgeMatrix(size);
            BuildDataMatrix(size, _buildMatrix);
            BuildDataMatrix(size, _destroyMatrix);
        }

        private static void CreateEdges()
        {
            _edgesToBeDestroyed = new Dictionary<string, Edge>();

            for (var row = 0; row < _adjacencyMatrix.GetLength(0); row++)
            {
                for (var col = 0; col < _adjacencyMatrix.GetLength(1); col++)
                {
                    if (_adjacencyMatrix[row, col])
                    {
                        var newEdge = new Edge(row, col, _destroyMatrix[row, col]);
                        _edgesToBeDestroyed.Add($"{row}-{col}", newEdge);
                    }
                }
            }
        }

        private static int FindRootFast(int node, int[] parents)
        {
            if (parents[node] == node)
            {
                return node;
            }

            return parents[node] = FindRootFast(parents[node], parents);
        }

        private static void Kruskal()
        {
            var nodes = _edgesToBeDestroyed.Select(x => x.Value.First)
                .Union(_edgesToBeDestroyed.Select(x => x.Value.Second))
                .Distinct()
                .ToArray();

            var max = nodes.Length;

            if (max != 0)
            {
                max = nodes.Max() + 1;
            }

            var parents = new int[max];

            //Set parent of node -> node
            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            var sortedKeys = _edgesToBeDestroyed
                .OrderByDescending(x => x.Value.Weight)
                .Select(x => x.Key)
                .ToList();

            while (sortedKeys.Count != 0)
            {
                var edgeKey = sortedKeys.First();
                var edge = _edgesToBeDestroyed[edgeKey];

                var firstNode = edge.First;
                var secondNode = edge.Second;

                sortedKeys.Remove($"{firstNode}-{secondNode}");
                sortedKeys.Remove($"{secondNode}-{firstNode}");

                var firstRoot = FindRootFast(firstNode, parents);
                var secondRoot = FindRootFast(secondNode, parents);

                if (firstRoot != secondRoot)
                {
                    _edgesToBeDestroyed.Remove($"{firstNode}-{secondNode}");
                    _edgesToBeDestroyed.Remove($"{secondNode}-{firstNode}");
                    parents[firstRoot] = secondRoot;
                }
            }
        }

        private static void RemoveEdgesFromAdjacencyMatrix()
        {
            foreach (var edge in _edgesToBeDestroyed.Values)
            {
                _adjacencyMatrix[edge.First, edge.Second] = false;
            }
        }

        private static void Dfs(int node, int id, Dictionary<int, HashSet<int>> connectedComponents, bool[] visited)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            for (var col = 0; col < _adjacencyMatrix.GetLength(1); col++)
            {
                if (_adjacencyMatrix[node, col])
                {
                    connectedComponents[id].Add(col);
                    Dfs(col, id, connectedComponents, visited);
                }
            }
        }

        private static int GetComponentId(Dictionary<int, HashSet<int>> connectedComponents, int element)
        {
            foreach (var id in connectedComponents.Keys)
            {
                if (connectedComponents[id].Contains(element))
                {
                    return id;
                }
            }

            return -1;
        }

        private static void MapConnectedComponents()
        {
            _edgesBetweenConnectedComponents = new List<Edge>();
            var connectedComponents = new Dictionary<int, HashSet<int>>();
            var nodes = _adjacencyMatrix.GetLength(0);
            var visited = new bool[nodes];
            var count = 0;

            for (var i = 0; i < nodes; i++)
            {
                if (!visited[i])
                {
                    connectedComponents[count] = new HashSet<int>();
                    connectedComponents[count].Add(i);
                    Dfs(i, count, connectedComponents, visited);
                    count++;
                }
            }

            foreach (var connectedComponent in connectedComponents)
            {
                var id = connectedComponent.Key;
                var component = connectedComponent.Value;

                foreach (var element in component)
                {
                    for (var col = 0; col < _adjacencyMatrix.GetLength(1); col++)
                    {
                        if (!_adjacencyMatrix[element, col] && !component.Contains(col))
                        {
                            var from = GetComponentId(connectedComponents, element);
                            var to = GetComponentId(connectedComponents, col);

                            var newEdge = new Edge(from, to, _buildMatrix[element, col]);
                            _edgesBetweenConnectedComponents.Add(newEdge);
                        }
                    }
                }
            }
        }

        private static void Kruskal2()
        {
            var nodes = _edgesBetweenConnectedComponents.Select(x => x.First)
                .Union(_edgesBetweenConnectedComponents.Select(x => x.Second))
                .Distinct()
                .ToArray();

            var parents = new int[nodes.Length];

            //Set parent of node -> node
            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            var edges = _edgesBetweenConnectedComponents
                .OrderBy(x => x.Weight)
                .ToList();

            _edgesToBeBuild = new List<Edge>();

            while (edges.Count != 0)
            {
                var edge = edges.First();
                edges.Remove(edge);

                var firstNode = edge.First;
                var secondNode = edge.Second;

                var firstRoot = FindRootFast(firstNode, parents);
                var secondRoot = FindRootFast(secondNode, parents);

                if (firstRoot != secondRoot)
                {
                    _edgesToBeBuild.Add(edge);
                    parents[firstRoot] = secondRoot;
                }
            }
        }

        public static void Main()
        {
            ReadInput();

            CreateEdges();

            Kruskal();

            RemoveEdgesFromAdjacencyMatrix();

            MapConnectedComponents();

            Kruskal2();

            var result = _edgesToBeDestroyed.Sum(x => x.Value.Weight) / 2.0;
            result += _edgesToBeBuild.Sum(x => x.Weight);
            Console.WriteLine(result);
        }
    }
}
