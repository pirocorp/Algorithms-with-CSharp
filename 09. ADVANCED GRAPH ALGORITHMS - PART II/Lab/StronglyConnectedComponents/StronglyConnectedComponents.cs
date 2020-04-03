using System;
using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents
{
    private static int size;
    private static bool[] visited;
    private static Stack<int> dfsNodesStack;
    private static List<int>[] reverseGraph;
    private static List<int>[] graph;
    private static List<List<int>> stronglyConnectedComponents;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        stronglyConnectedComponents = new List<List<int>>();
        dfsNodesStack = new Stack<int>();
        graph = targetGraph;
        size = graph.Length;
        BuildReverseGraph();

        //Traverse the graph with DFS and push all nodes in the stack
        //in post-order (on return from recursion)
        visited = new bool[size];
        for (var node = 0; node < size; node++)
        {
            if (!visited[node])
            {
                DFS(node);
            }
        }

        visited = new bool[size];
        while (dfsNodesStack.Count > 0)
        {
            var node = dfsNodesStack.Pop();
            if (!visited[node])
            {
                stronglyConnectedComponents.Add(new List<int>());
                ReverseDFS(node);
            }
        }

        return stronglyConnectedComponents;
    }

    private static void ReverseDFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            stronglyConnectedComponents.Last().Add(node);

            foreach (var childNode in reverseGraph[node])
            {
                ReverseDFS(childNode);
            }
        }
    }

    private static void DFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            foreach (var childNode in graph[node])
            {
                DFS(childNode);
            }

            dfsNodesStack.Push(node);
        }
    }

    private static void BuildReverseGraph()
    {
        reverseGraph = new List<int>[size];

        for (int node = 0; node < size; node++)
        {
            reverseGraph[node] = new List<int>();
        }

        for (int node = 0; node < size; node++)
        {
            foreach (var childNode in graph[node])
            {
                reverseGraph[childNode].Add(node);
            }
        }
    }
}
