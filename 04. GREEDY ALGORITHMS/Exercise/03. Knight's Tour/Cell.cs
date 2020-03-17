namespace _03._Knight_s_Tour
{
    public class Cell
    {
        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; }

        public int Col { get; }

        public override string ToString()
        {
            return $"Row: {this.Row}, Col: {this.Col}";
        }
    }
}
