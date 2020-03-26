namespace _02._Graph_Traversals
{
    using System;
    using System.Collections.Generic;

    public static class GraphTraversalsProgram
    {
        private static bool[] _visited;
        private static List<int>[] _graph;

        public static void Main()
        {
            _graph = new List<int>[]
            {
                new List<int> {3, 6},           //0
                new List<int> {2, 3, 4, 5, 6},  //1
                new List<int> {1, 4, 5},        //2
                new List<int> {0, 1, 5},        //3
                new List<int> {1, 2, 6},        //4
                new List<int> {1, 2, 3},        //5
                new List<int> {0, 1, 4},        //6

                new List<int> {8},              //7
                new List<int> {7, 9},           //8
                new List<int> {8},              //9
            };

            _visited = new bool[_graph.Length];

            var count = 0;
            for (var i = 0; i < _graph.Length; i++)
            {
                if (!_visited[i])
                {
                    Console.WriteLine($"Connected component {++count}: ");
                    Dfs(i);
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            _visited = new bool[_graph.Length];


            for (var i = 0; i < _graph.Length; i++)
            {
                Bfs(i);
            }

            Console.WriteLine();
            _visited = new bool[_graph.Length];


            for (var i = 0; i < _graph.Length; i++)
            {
                NoneRecursiveDfs(i);
            }
        }

        private static void Dfs(int node)
        {
            if (!_visited[node])
            {
                _visited[node] = true;
                var children = _graph[node];

                foreach (var child in children)
                {
                    Dfs(child);
                }

                Console.Write($"{node} ");
            }
        }

        private static void Bfs(int node)
        {
            if (_visited[node])
            {
                return;
            }

            var queue = new Queue<int>();
            queue.Enqueue(node);
            _visited[node] = true;

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                Console.Write($"{current} ");

                foreach (var child in _graph[current])
                {
                    if (_visited[child])
                    {
                        continue;
                    }

                    queue.Enqueue(child);
                    _visited[child] = true;
                }
            }
        }

        private static void NoneRecursiveDfs(int node)
        {
            if (_visited[node])
            {
                return;
            }

            var stack = new Stack<int>();
            stack.Push(node);
            _visited[node] = true;

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                Console.Write($"{current} ");

                foreach (var child in _graph[current])
                {
                    if (_visited[child])
                    {
                        continue;
                    }

                    stack.Push(child);
                    _visited[child] = true;
                }
            }
        }
    }
}