namespace _03._Renewal
{
    using System;

    public class Edge
    {
        public Edge(int first, int second, int weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public int First { get; }

        public int Second { get; }

        public int Weight { get; }

        public override string ToString()
        {
            return $"{this.First} -> {this.Second}, {this.Weight}";
        }
    }
}