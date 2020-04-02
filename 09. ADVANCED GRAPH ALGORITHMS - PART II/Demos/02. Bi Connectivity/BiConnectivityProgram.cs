namespace _02._Bi_Connectivity
{
    using System;
    using System.Collections.Generic;

    public static class BiConnectivityProgram
    {
        private static readonly List<int>[] _graph = new List<int>[]
        {
            new List<int>() {1, 2, 6, 7, 9},      // children of node 0
            new List<int>() {0, 6},               // children of node 1
            new List<int>() {0, 7},               // children of node 2
            new List<int>() {4},                  // children of node 3
            new List<int>() {3, 6, 10},           // children of node 4
            new List<int>() {7},                  // children of node 5
            new List<int>() {0, 1, 4, 8, 10, 11}, // children of node 6
            new List<int>() {0, 2, 5, 9},         // children of node 7
            new List<int>() {6, 11},              // children of node 8
            new List<int>() {0, 7},               // children of node 9
            new List<int>() {4, 6},               // children of node 10
            new List<int>() {6, 8},               // children of node 11
        };
        private static bool[] _visited;
        private static int[] _depths;
        private static int[] _lowPoints;
        private static int?[] _parents;

        private static List<int> _articulationPoints;

        private static void FindArticulationPoints(int node, int depth)
        {
            _visited[node] = true;
            _depths[node] = depth;
            _lowPoints[node] = depth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in _graph[node])
            {
                if (!_visited[child])
                {
                    _parents[child] = node;
                    FindArticulationPoints(child, depth + 1);
                    childCount++;

                    if (_lowPoints[child] >= _depths[node])
                    {
                        isArticulationPoint = true;
                    }

                    _lowPoints[node] = Math.Min(_lowPoints[node], _lowPoints[child]);
                }
                else if(child != _parents[node])
                {
                    _lowPoints[node] = Math.Min(_lowPoints[node], _depths[child]);
                }
            }

            if ((_parents[node] == null && childCount > 1)
                || (_parents[node] != null && isArticulationPoint))
            {
                _articulationPoints.Add(node);
            }
        }

        private static void FindArticulationPoints()
        {
            _visited = new bool[_graph.Length];
            _depths = new int[_graph.Length];
            _lowPoints = new int[_graph.Length];
            _parents = new int?[_graph.Length];
            _articulationPoints = new List<int>();

            for (var node = 0; node < _graph.Length; node++)
            {
                if (!_visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }
        }

        public static void Main()
        {
            FindArticulationPoints();
            Console.WriteLine("Articulation points: " +
                              string.Join(", ", _articulationPoints));
        }
    }
}
