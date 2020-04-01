using System;
using System.Collections.Generic;

public class KruskalAlgorithm
{
    public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
    {
        //Initialize parents
        var parents = new int[numberOfVertices];

        for (var i = 0; i < numberOfVertices; i++)
        {
            parents[i] = i;
        }

        //Kruskal’s Algorithm
        var spanningTree = new List<Edge>();

        edges.Sort();

        foreach (var edge in edges)
        {
            int rootStartNode = FindRoot(edge.StartNode, parents);
            int rootEndNode = FindRoot(edge.EndNode, parents);

            if (rootStartNode != rootEndNode) //No cycle
            {
                spanningTree.Add(edge);
                parents[rootEndNode] = rootStartNode;
            }
        }

        return spanningTree;
    }

    public static int FindRoot(int node, int[] parent)
    {
        if (parent[node] == node)
        {
            return node;
        }

        //Path Compression
        return parent[node] = FindRoot(parent[node], parent);
    }
}