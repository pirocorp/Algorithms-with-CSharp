namespace _01._Graph_Representation
{
    public class Edge
    {
        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        public int From { get; private set; }

        public int To { get; private set; }
    }
}
