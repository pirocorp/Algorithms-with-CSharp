using System;
using System.Collections.Generic;

public static class DijkstraWithoutQueue
{
    public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
    {
        int n = graph.GetLength(0);

        //Initialize th distance[]
        int[] distance = new int[n];

        for (var i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }

        distance[sourceNode] = 0;

        var used = new bool[n];
        int?[] previous = new int?[n];

        //Find Nearest unvisited node from the source
        while (true)
        {
            int minDistance = int.MaxValue;
            int minNode = 0;

            for (int node = 0; node < n; node++)
            {
                if (!used[node] && distance[node] < minDistance)
                {
                    minDistance = distance[node];
                    minNode = node;
                }
            }

            if (minDistance == int.MaxValue)
            {
                //No min distance node found --> algo finished
                break;
            }

            used[minNode] = true;

            //Improve the distance[0, n-1] through minNode
            for (int i = 0; i < n; i++)
            {
                if (graph[minNode, i] > 0) //node is connected to minNode
                {
                    var newDistance = minDistance + graph[minNode, i];

                    if (newDistance < distance[i])
                    {
                        distance[i] = newDistance;
                        previous[i] = minNode;
                    }
                }
            }
        }

        if (distance[destinationNode] == int.MaxValue)
        {
            //No path found from sourceNode to destinationNode
            return null;
        }

        //Reconstruct the shortest path from sourceNode to destinationNode
        var path = new List<int>();
        int? currentNode = destinationNode;
        while (currentNode != null)
        {
            path.Add(currentNode.Value);
            currentNode = previous[currentNode.Value];
        }

        path.Reverse();
        return path;
    }
}
