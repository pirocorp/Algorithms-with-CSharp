namespace _02._Fast_and_Furious
{
    public class Edge
    {
        public Edge(string first, string second, decimal weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public string First { get; }

        public string Second { get; }

        public decimal Weight { get; }

        public override string ToString()
        {
            return $"{this.First} -> {this.Second}, {this.Weight}";
        }
    }
}