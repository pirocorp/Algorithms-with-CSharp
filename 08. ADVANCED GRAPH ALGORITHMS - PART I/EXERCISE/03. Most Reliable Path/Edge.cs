namespace _03._Most_Reliable_Path
{
    public class Edge
    {
        private readonly decimal _weight;

        public Edge(int first, int second, decimal weight)
        {
            this.First = first;
            this.Second = second;
            this._weight = weight;
        }

        public int First { get; }

        public int Second { get; }

        public decimal Weight => this._weight / 100;

        public override string ToString()
        {
            return $"({this.First} {this.Second}) -> {this.Weight}";
        }
    }
}
