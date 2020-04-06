namespace _01._Maximum_Tasks_Assignment
{
    using System;
    using System.Collections.Generic;

    public static class MaximumTasksAssignmentProgram
    {
        private static int[][] _graph;
        private static int[] _parents;
        private static int _people;
        private static int _tasks;

        private static void ReadInput()
        {
            _people = int.Parse(Console.ReadLine().Split(' ')[1]);
            _tasks = int.Parse(Console.ReadLine().Split(' ')[1]);

            var nodes = _people + _tasks + 2;

            //n = 3 - S A B C 1 2 3 E
            _graph = new int[nodes][];

            for (var i = 0; i < _graph.Length; i++)
            {
                _graph[i] = new int[nodes];
            }

            //Add edges from start to A, B, C
            for (var i = 0; i < _people; i++)
            {
                _graph[0][i + 1] = 1;
            }

            //Add edges from 1, 2, 3 to end
            for (var i = 0; i < _tasks; i++)
            {
                _graph[i + _people + 1][_graph.Length - 1] = 1;
            }

            for (var person = 0; person < _people; person++)
            {
                var currentLine = Console.ReadLine();

                for (var task = 0; task < _tasks; task++)
                {
                    if (currentLine[task] == 'Y')
                    {
                        _graph[person + 1][task + _people + 1] = 1;
                    }
                }
            }

            _parents = new int[nodes];

            for (var i = 0; i < _parents.Length; i++)
            {
                _parents[i] = -1;
            }
        }

        private static bool Bfs(int start, int end)
        {
            var visited = new bool[_graph.Length];

            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (var child = 0; child < _graph.Length; child++)
                {
                    if (!visited[child] 
                        && _graph[node][child] > 0)
                    {
                        queue.Enqueue(child);
                        visited[child] = true;
                        _parents[child] = node;
                    }
                }
            }

            return visited[end];
        }

        private static void MaxFlow()
        {
            var start = 0;
            var end = _graph.Length - 1;

            while (Bfs(start, end))
            {
                var currentNode = end;

                while (currentNode != start)
                {
                    var prevNode = _parents[currentNode];

                    _graph[prevNode][currentNode] = 0;
                    _graph[currentNode][prevNode] = 1;

                    currentNode = prevNode;
                }
            }
        }

        private static SortedSet<string> ReconstructSolution()
        {
            var queue = new Queue<int>();
            var result = new SortedSet<string>();
            var visited = new bool[_graph.Length];
            var start = 0;
            var end = _graph.Length - 1;

            queue.Enqueue(end);
            visited[end] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (var child = 0; child < _graph.Length; child++)
                {
                    if (_graph[node][child] > 0
                        && !visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;

                        if (node != end && node != start
                                        && child != end && child != start)
                        {
                            result.Add($"{(char)(child - 1 + 'A')}-{node - _people}");
                        }
                    }
                }
            }

            return result;
        }

        private static void FirstSolution()
        {
            ReadInput();
            MaxFlow();

            var result = ReconstructSolution();
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        public static void Main()
        {
            FirstSolution();
        }
    }
}
