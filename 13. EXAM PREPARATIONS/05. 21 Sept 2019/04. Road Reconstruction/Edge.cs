namespace _04._Road_Reconstruction
{
    public class Edge
    {
        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        public int From { get; }

        public int To { get; }

        public override string ToString()
        {
            return $"{this.From} -> {this.To}";
        }
    }
}
