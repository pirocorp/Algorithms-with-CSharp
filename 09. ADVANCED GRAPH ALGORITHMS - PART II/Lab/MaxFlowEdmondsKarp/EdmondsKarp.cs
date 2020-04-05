 using System.Collections.Generic;

public class EdmondsKarp
{
    private static int[][] graph;
    private static int[] parents;

    public static int FindMaxFlow(int[][] targetGraph)
    {
        graph = targetGraph;
        parents = new int[graph.Length];

        //Initialize parents
        for (int i = 0; i < graph.Length; i++)
        {
            parents[i] = -1;
        }

        var maxFlow = 0;
        var start = 0;
        var end = graph.Length - 1;

        while (Bfs(start, end))
        {
            var pathFlow = int.MaxValue;
            var currentNode = end;

            while (currentNode != start)
            {
                var previousNode = parents[currentNode];
                var currentFlow = graph[previousNode][currentNode];

                if (currentFlow > 0 && currentFlow < pathFlow)
                {
                    pathFlow = currentFlow;
                }

                currentNode = previousNode;
            }

            maxFlow += pathFlow;
            currentNode = end;

            while (currentNode != start)
            {
                var prevNode = parents[currentNode];

                graph[prevNode][currentNode] -= pathFlow;
                graph[currentNode][prevNode] += pathFlow;

                currentNode = prevNode;
            }
        }

        return maxFlow;
    }

    private static bool Bfs(int start, int end)
    {
        var visited = new bool[graph.Length];

        var queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            for (var child = 0; child < graph[node].Length; child++)
            {
                if (graph[node][child] > 0 && !visited[child])
                {
                    queue.Enqueue(child);
                    parents[child] = node;
                    visited[child] = true;
                }
            }
        }

        return visited[end];
    }
}
