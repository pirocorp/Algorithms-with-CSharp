namespace _03._Evacuation
{
    using System;

    public class Edge
    {
        public Edge(int from, int to, TimeSpan weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public int From { get; }

        public int To { get; }

        public TimeSpan Weight { get; }

        public override string ToString()
        {
            return $"{this.From} -> {this.To}, {this.Weight}";
        }
    }
}
