namespace _02._Find_Bi_Connected_Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FindBiConnectedComponentsProgram
    {
        private static bool[] _visited;
        private static int[] _depths;
        private static int[] _lowPoints;
        private static int[] _parents;
        private static List<int>[] _graph;
        private static Stack<KeyValuePair<int, int>> _biConnected;
        private static List<List<int>> _components;

        private static void ReadInput()
        {
            var nodes = int.Parse(Console.ReadLine().Split(' ')[1]);
            var edges = int.Parse(Console.ReadLine().Split(' ')[1]);

            _biConnected = new Stack<KeyValuePair<int, int>>();
            _components = new List<List<int>>();

            _visited = new bool[nodes];
            _depths = new int[nodes];
            _lowPoints = new int[nodes];
            _parents = Enumerable.Repeat(-1, nodes).ToArray();

            _graph = new List<int>[nodes];

            for (var i = 0; i < _graph.Length; i++)
            {
                _graph[i] = new List<int>();
            }

            for (var i = 0; i < edges; i++)
            {
                var edgeTokens = Console.ReadLine().Split(' ');
                var parent = int.Parse(edgeTokens[0]);
                var child = int.Parse(edgeTokens[1]);

                _graph[parent].Add(child);
                _graph[child].Add(parent);
            }
        }

        private static void FindBiConnectedComponents(int node, int depth)
        {
            _visited[node] = true;
            _depths[node] = depth;
            _lowPoints[node] = depth;

            foreach (var childNode in _graph[node])
            {
                if (!_visited[childNode])
                {
                    _parents[childNode] = node;
                    FindBiConnectedComponents(childNode, depth + 1);

                    _biConnected.Push(new KeyValuePair<int, int>(node, childNode));

                    if (_lowPoints[childNode] >= _depths[node])
                    {
                        if (_biConnected.Count > 0)
                        {
                            var edge = _biConnected.Peek();
                            _components.Add(new List<int>());
                            _components[_components.Count - 1].Add(edge.Key);
                            //Console.Write($"{{{edge.Key}");

                            do
                            {
                                //Console.Write($", ");
                                edge = _biConnected.Pop();
                                _components[_components.Count - 1].Add(edge.Value);
                                //Console.Write($"{edge.Value}");

                            } while (_biConnected.Count > 0 && (edge.Key != node || edge.Value == _biConnected.Peek().Key));

                            //Console.WriteLine($"}}");
                        }
                    }

                    _lowPoints[node] = Math.Min(_lowPoints[node], _lowPoints[childNode]);
                }
                else if (childNode != _parents[node])
                {
                    _lowPoints[node] = Math.Min(_lowPoints[node], _depths[childNode]);
                }
            }
        }

        public static void Main()
        {
            ReadInput();
            FindBiConnectedComponents(0, 1);

            //foreach (var component in _components)
            //{
            //    Console.WriteLine($"{{{string.Join(", ", component)}}}");
            //}

            Console.WriteLine($"Number of bi-connected components: {_components.Count}");
        }
    }
}