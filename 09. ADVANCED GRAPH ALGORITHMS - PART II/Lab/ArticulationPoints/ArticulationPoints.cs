using System;
using System.Collections.Generic;

public class ArticulationPoints
{
    private static List<int>[] _graph;
    private static bool[] _visited;
    private static int[] _depths;
    private static int[] _lowPoints;
    private static int?[] _parents;
    private static List<int> _articulationPoints;

    public static List<int> FindArticulationPoints(List<int>[] targetGraph)
    {
        _graph = targetGraph;
        _visited = new bool[_graph.Length];
        _depths = new int[_graph.Length];
        _lowPoints = new int[_graph.Length];
        _parents = new int?[_graph.Length];
        _articulationPoints = new List<int>();

        for (var node = 0; node < _graph.Length; node++)
        {
            if (!_visited[node])
            {
                FindArticulationPoints(node, 1);
            }
        }

        return _articulationPoints;
    }

    private static void FindArticulationPoints(int node, int depth)
    {
        _visited[node] = true;
        _depths[node] = depth;
        _lowPoints[node] = depth;

        var childCount = 0;
        var isArticulationPoint = false;

        foreach (var child in _graph[node])
        {
            if (!_visited[child])
            {
                _parents[child] = node;
                FindArticulationPoints(child, depth + 1);
                childCount++;

                if (_lowPoints[child] >= _depths[node])
                {
                    isArticulationPoint = true;
                }

                _lowPoints[node] = Math.Min(_lowPoints[node], _lowPoints[child]);
            }
            else if (child != _parents[node])
            {
                _lowPoints[node] = Math.Min(_lowPoints[node], _depths[child]);
            }
        }

        if ((_parents[node] == null && childCount > 1)
            || (_parents[node] != null && isArticulationPoint))
        {
            _articulationPoints.Add(node);
        }
    }
}
