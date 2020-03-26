using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private HashSet<string> visited;
    private HashSet<string> cycleNodes;
    private Dictionary<string, int> predecessorCount;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
        //this.GetPredecessorCount();
        this.visited = new HashSet<string>();
        this.cycleNodes = new HashSet<string>();
    }

    public ICollection<string> TopSort()
    {
        var sorted = new LinkedList<string>();

        foreach (var node in this.graph.Keys)
        {
            DFS(node, sorted);
        }

        return sorted;

        //return this.SourceRemovalTopologicalSorting();
    }

    private void DFS(string node, LinkedList<string> result)
    {
        if (this.cycleNodes.Contains(node))
        {
            throw new InvalidOperationException();
        }

        if (!this.visited.Contains(node))
        {
            this.visited.Add(node);
            this.cycleNodes.Add(node);

            foreach (var child in this.graph[node])
            {
                DFS(child, result);
            }

            this.cycleNodes.Remove(node);
            result.AddFirst(node);
        }
    }

    private ICollection<string> SourceRemovalTopologicalSorting()
    {
        var sorted = new List<string>();

        while (true)
        {
            var nodeToRemove = this.predecessorCount.Keys
                .FirstOrDefault(x => this.predecessorCount[x] == 0);

            if (nodeToRemove == null)
            {
                break;
            }

            this.graph.Remove(nodeToRemove);
            this.GetPredecessorCount();
            sorted.Add(nodeToRemove);
        }

        if (this.graph.Count > 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    private void GetPredecessorCount()
    {
        this.predecessorCount = new Dictionary<string, int>();

        foreach (var node in graph)
        {
            if (!this.predecessorCount.ContainsKey(node.Key))
            {
                this.predecessorCount[node.Key] = 0;
            }

            foreach (var child in node.Value)
            {
                if (!this.predecessorCount.ContainsKey(child))
                {
                    this.predecessorCount[child] = 0;
                }

                this.predecessorCount[child]++;
            }
        }
    }
}
