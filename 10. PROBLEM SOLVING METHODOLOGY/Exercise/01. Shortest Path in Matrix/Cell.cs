namespace _01._Shortest_Path_in_Matrix
{
    using System;

    public class Cell : IComparable<Cell>
    {
        public Cell(int row, int col, int value)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
        }

        public int Row { get; }

        public int Col { get; }

        public int Value { get; set; }

        public int CompareTo(Cell other)
        {
            var cmp = this.Value.CompareTo(other.Value);

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

        public override int GetHashCode()
        {
            return this.Row ^ this.Col;
        }

        public override bool Equals(object obj)
        {
            return this.EqualsDerived(obj) && obj.GetType() == typeof(Cell);
        }

        protected virtual bool EqualsDerived(object obj)
        {
            return !ReferenceEquals(obj, null) 
                   && obj is Cell 
                   && ((Cell)obj).Row == this.Row
                   && ((Cell)obj).Col == this.Col;
        }

        public static bool operator ==(Cell pt1, Cell pt2)
        {

            if (ReferenceEquals(pt1, pt2))
            {
                return true;
            }

            if (!ReferenceEquals(pt1, null) &&
                !ReferenceEquals(pt2, null) &&
                pt1.Equals(pt2))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Cell pt1, Cell pt2)
        {
            return !(pt1 == pt2);
        }

        public override string ToString()
        {
            return $"({this.Row}, {this.Col}): {this.Value}";
        }
    }
}