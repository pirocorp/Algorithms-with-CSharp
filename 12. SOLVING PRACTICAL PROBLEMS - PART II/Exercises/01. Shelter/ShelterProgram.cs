namespace _01._Shelter
{
    using System;
    using System.Collections.Generic;

    public static class ShelterProgram
    {

        private static int[] work;

        private static int[] bfsDist;

        private static int capacity;

        private static int endNode;

        private static List<int>[] edges;

        private static int[][] capacities;

        private static int[][] distanceMatrix;


        static void Main(string[] args)
        {
            var tokens = Console.ReadLine().Split();
            var soldiersCount = int.Parse(tokens[0]);
            var bunkersCount = int.Parse(tokens[1]);
            capacity = int.Parse(tokens[2]);
            var soldiers = new Point[soldiersCount + 1];
            var distances = new List<int>();

            for (var i = 1; i <= soldiersCount; i++)
            {
                //Soldiers will be numbered 1 to N
                var soldierTokens = Console.ReadLine().Split();
                var x = int.Parse(soldierTokens[0]);
                var y = int.Parse(soldierTokens[1]);
                soldiers[i] = new Point(x, y);
            }

            distanceMatrix = new int[bunkersCount + 1][];
            for (var i = 1; i <= bunkersCount; i++)
            {
                var bunkerTokens = Console.ReadLine().Split();
                distanceMatrix[i] = new int[soldiersCount + 1];
                for (var j = 1; j <= soldiersCount; j++)
                {
                    var x = int.Parse(bunkerTokens[0]);
                    var y = int.Parse(bunkerTokens[1]);
                    var distanceX = x - soldiers[j].X;
                    var distanceY = y - soldiers[j].Y;
                    //Get distance between soldier and bunker
                    var distance = distanceX * distanceX + distanceY * distanceY;
                    distances.Add(distance);
                    distanceMatrix[i][j] = distance;
                }

            }

            endNode = soldiersCount + bunkersCount + 1;
            work = new int[endNode + 1];
            bfsDist = new int[endNode + 1];
            capacities = new int[endNode + 1][];
            for (var i = 0; i <= endNode; i++)
            {
                capacities[i] = new int[endNode + 1];
            }

            distances.Sort();
            //binary search
            var bestDistance = distances[distances.Count - 1];
            var low = 0;
            var high = distances.Count - 1;
            while (low <= high)
            {
                var mid = (low + high) / 2;
                var res = DinicConstrained(distances[mid], soldiersCount, bunkersCount);
                if (res == soldiersCount)
                {
                    bestDistance = Math.Min(bestDistance, distances[mid]);
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

            }

            Console.WriteLine("{0:F6}", Math.Sqrt(bestDistance));
        }

        static int DinicConstrained(int maxWeight, int soldiersCount, int bunkersCount)
        {
            edges = new List<int>[endNode + 1];
            edges[0] = new List<int>();
            for (var i = 1; i <= soldiersCount; i++)
            {
                edges[i] = new List<int>();
                edges[0].Add(i);
                edges[i].Add(0);
                capacities[0][i] = 1;
                capacities[i][0] = 0;
            }

            edges[endNode] = new List<int>();
            for (var i = 1; i <= bunkersCount; i++)
            {
                //Bunkers will be numbered N + 1 to N + M
                edges[soldiersCount + i] = new List<int>();
                edges[soldiersCount + i].Add(endNode);
                edges[endNode].Add(soldiersCount + i);
                capacities[soldiersCount + i][endNode] = capacity;
                capacities[endNode][soldiersCount + i] = 0;

                for (var j = 1; j <= soldiersCount; j++)
                {
                    if (distanceMatrix[i][j] <= maxWeight)
                    {
                        edges[j].Add(soldiersCount + i);
                        edges[soldiersCount + i].Add(j);
                        capacities[j][soldiersCount + i] = 1;
                        capacities[soldiersCount + i][j] = 0;
                    }
                }
            }

            return Dinic(0, endNode);
        }

        static int Dinic(int source, int destination)
        {
            var result = 0;
            while (Bfs(source, destination))
            {
                for (var i = 0; i < work.Length; i++)
                {
                    work[i] = 0;
                }

                int delta;
                do
                {
                    delta = Dfs(source, int.MaxValue);
                    result += delta;
                }
                while (delta != 0);
            }
            return result;
        }

        static bool Bfs(int src, int dest)
        {
            for (var i = 0; i < bfsDist.Length; i++)
            {
                bfsDist[i] = -1;
            }
            bfsDist[src] = 0;
            var queue = new Queue<int>();
            queue.Enqueue(src);
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                for (var i = 0; i < edges[currentNode].Count; i++)
                {
                    var child = edges[currentNode][i];
                    if (bfsDist[child] < 0 && capacities[currentNode][child] > 0)
                    {
                        bfsDist[child] = bfsDist[currentNode] + 1;
                        queue.Enqueue(child);
                    }
                }
            }
            return bfsDist[dest] >= 0;
        }

        static int Dfs(int source, int flow)
        {
            if (source == endNode)
            {
                return flow;
            }
            for (var i = work[source]; i < edges[source].Count; i++, work[source]++)
            {
                var child = edges[source][i];
                if (capacities[source][child] <= 0) continue;
                if (bfsDist[child] == bfsDist[source] + 1)
                {
                    var augmentationPathFlow = Dfs(child, Math.Min(flow, capacities[source][child]));
                    if (augmentationPathFlow > 0)
                    {
                        capacities[source][child] -= augmentationPathFlow;
                        capacities[child][source] += augmentationPathFlow;
                        return augmentationPathFlow;
                    }
                }
            }
            return 0;
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }

    public class Shelter
    {
        public Shelter(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }

    public class Soldier
    {
        public Soldier(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}