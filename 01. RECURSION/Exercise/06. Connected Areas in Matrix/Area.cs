namespace _06._Connected_Areas_in_Matrix
{
    using System;

    public class Area : IComparable<Area>
    {
        public Area(int row, int col, int size)
        {
            this.Row = row;
            this.Col = col;
            this.Size = size;
        }

        public int Row { get; private set; }

        public int Col { get; private set; }

        public int Size { get; private set; }

        public int CompareTo(Area other)
        {
            var cmp = -this.Size.CompareTo(other.Size);

            if (cmp == 0)
            {
                cmp = this.Row.CompareTo(other.Row);
            }

            if (cmp == 0)
            {
                cmp = this.Col.CompareTo(other.Col);
            }

            return cmp;
        }
    }
}
