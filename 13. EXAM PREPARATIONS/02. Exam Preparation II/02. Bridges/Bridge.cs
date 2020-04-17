namespace _02._Bridges
{
    public class Bridge
    {
        public Bridge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }

        public int From { get; }

        public int To { get; }

        public int Length => this.To - this.From;

        public override string ToString()
        {
            return $"{this.From} -> {this.To}";
        }
    }
}
