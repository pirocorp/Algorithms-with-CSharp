namespace _03._Max_Flow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MaxFlowProgram
    {
        private static int[][] _graph = new int[][]
        {
            new int[] { 0, 10, 10, 0, 0, 0 },
            new int[] { 0, 0, 2, 4, 8, 0},
            new int[] { 0, 0, 0, 0, 9, 0},
            new int[] { 0, 0, 0, 0, 0, 10 },
            new int[] { 0, 0, 0, 6, 0, 10 },
            new int[] { 0, 0, 0, 0, 0, 0 },
        };
        private static int[] _parent;

        private static bool Bfs(int start, int end)
        {
            var visited = new bool[_graph.Length];
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (var child = 0; child < _graph[node].Length; child++)
                {
                    if (_graph[node][child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);
                        _parent[child] = node;
                        visited[child] = true;
                    }
                }
            }

            return visited[end];
        }

        private static int FindMaxFlow()
        {
            _parent = Enumerable
                .Repeat(-1, _graph.Length)
                .ToArray();

            var maxFlow = 0;
            var start = 0;
            var end = _graph.Length - 1;

            //Bfs check if path still exists between start and end
            //and if exists map it in _parent[]
            while (Bfs(start, end))
            {
                var pathFlow = int.MaxValue;
                var currentNode = end;

                //first while finds current path flow
                while (currentNode != start)
                {
                    var prevNode = _parent[currentNode];
                    var currentFlow = _graph[prevNode][currentNode];

                    if (currentFlow > 0
                        && currentFlow < pathFlow)
                    {
                        pathFlow = currentFlow;
                    }

                    currentNode = prevNode;
                }

                maxFlow += pathFlow;
                currentNode = end;

                //second while modifies 
                while (currentNode != start)
                {
                    var prevNode = _parent[currentNode];

                    _graph[prevNode][currentNode] -= pathFlow;
                    _graph[currentNode][prevNode] += pathFlow;

                    currentNode = prevNode;
                }
            }

            return maxFlow;
        }

        public static void Main()
        {
            _graph = new int[][]
            {
                new int[]{ 0, 1, 1, 1, 0, 0, 0, 0 },
                new int[]{ 0, 0, 0, 0, 1, 0, 1, 0 },
                new int[]{ 0, 0, 0, 0, 0, 1, 1, 0 },
                new int[]{ 0, 0, 0, 0, 1, 1, 0, 0 },
                new int[]{ 0, 0, 0, 0, 0, 0, 0, 1 },
                new int[]{ 0, 0, 0, 0, 0, 0, 0, 1 },
                new int[]{ 0, 0, 0, 0, 0, 0, 0, 1 },
                new int[]{ 0, 0, 0, 0, 0, 0, 0, 0 },
            };

            var maxFlow = FindMaxFlow();
            Console.WriteLine($"Max flow = {maxFlow}");
        }
    }
}
