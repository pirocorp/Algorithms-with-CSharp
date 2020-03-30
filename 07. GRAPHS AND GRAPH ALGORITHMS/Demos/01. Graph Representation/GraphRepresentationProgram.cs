namespace _01._Graph_Representation
{
    using System.Collections.Generic;
    using System.Linq;

    public static class GraphRepresentationProgram
    {
        public static void Main()
        {
            AdjacencyListGraphRepresentation();
            AdjacencyMatrixGraphRepresentation();
            ListOfEdgesGraphRepresentation();
            GraphOfNamedNodes();
            GraphOfNumberedNodes();
        }

        private static void AdjacencyListGraphRepresentation()
        {
            //Graph Representation: Adjacency List
            var graph = new List<int>[]
            {
                new List<int> {3, 6},
                new List<int> {2, 3, 4, 5, 6},
                new List<int> {1, 4, 5},
                new List<int> {0, 1, 5},
                new List<int> {1, 2, 6},
                new List<int> {1, 2, 3},
                new List<int> {0, 1, 4},
            };

            // Add an edge { 3 -> 6 }
            graph[3].Add(6);

            // List the children of node #1
            var childNodes = graph[1];
        }

        private static void AdjacencyMatrixGraphRepresentation()
        {
            var graph = new[,]
            {
                // 0  1  2  3  4  5  6
                {0, 0, 0, 1, 0, 0, 1}, // node 0
                {0, 0, 1, 1, 1, 1, 1}, // node 1
                {0, 1, 0, 0, 1, 1, 0}, // node 2
                {1, 1, 0, 0, 0, 1, 0}, // node 3
                {0, 1, 1, 0, 0, 0, 1}, // node 4
                {0, 1, 1, 1, 0, 0, 0}, // node 5
                {1, 1, 0, 0, 1, 0, 0}, // node 6
            };
            // Add an edge { 3 -> 6 }
            graph[3, 6] = 1;

            // List the children of node #1
            for (var i = 0; i < graph.GetLength(1); i++)
            {
                var childNode = graph[1, i];
            }
        }

        private static void ListOfEdgesGraphRepresentation()
        {
            var graph = new List<Edge>()
            {
                new Edge(0, 3),
                new Edge(0, 6),
            };

            // Add an edge { 3 -> 6 }
            graph.Add(new Edge(3, 6));

            // List the children of node #1
            var childNodes = graph
                .Where(e => e.From == 1)
                .ToArray();
        }

        private static void GraphOfNamedNodes()
        {
            var graph = new Dictionary<string, List<string>>()
            {
                {
                    "Sofia", new List<string>()
                    {
                        "Plovdiv", "Varna", "Bourgas", "Pleven", "Stara Zagora"
                    }
                },
                {
                    "Plovdiv", new List<string>()
                    {
                        "Bourgas", "Ruse"
                    }
                },
                {
                    "Varna", new List<string>()
                    {
                        "Ruse", "Stara Zagora"
                    }
                },
                {
                    "Bourgas", new List<string>()
                    {
                        "Plovdiv", "Pleven"
                    }
                },
                {
                    "Ruse", new List<string>()
                    {
                        "Varna", "Plovdiv"
                    }
                },
                {
                    "Pleven", new List<string>()
                    {
                        "Bourgas", "Stara Zagora"
                    }
                },
                {
                    "Stara Zagora", new List<string>()
                    {
                        "Varna", "Pleven"
                    }
                },
            };
        }

        private static void GraphOfNumberedNodes()
        {
            var nodes = new List<int>[]
            {
                new List<int> {3, 6}, // children of node 0 (Ruse)
                new List<int> {2, 3, 4, 5, 6}, // successors of node 1 (Sofia)
                new List<int> {1, 4, 5}, // successors of node 2 (Pleven)
                new List<int> {0, 1, 5}, // successors of node 3 (Varna)
                new List<int> {1, 2, 6}, // successors of node 4 (Bourgas)
                new List<int> {1, 2, 3}, // successors of node 5 (Stara Zagora)
                new List<int> {0, 1, 4} // successors of node 6 (Plovdiv)
            };

            var names = new string[] { "Ruse", "Sofia", "Pleven", "Varna", "Bourgas", "Stara Zagora", "Plovdiv" };

            var graph = new Graph(nodes, names);
        }
    }
}
