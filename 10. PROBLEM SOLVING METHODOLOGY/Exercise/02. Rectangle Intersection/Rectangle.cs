namespace _02._Rectangle_Intersection
{
    using System;
    
    public class Rectangle : IComparable<Rectangle>
    {
        public Rectangle(int minX, int maxX, int minY, int maxY)
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }

        public int MinX { get; }

        public int MaxX { get; }

        public int MinY { get; }

        public int MaxY { get; }

        public int CompareTo(Rectangle other)
        {
            return this.MinX.CompareTo(other.MinX);
        }

        public decimal CalculatedArea()
        {
            return Math.Abs((this.MaxX - this.MinX) * (this.MaxY - this.MinY));
        }
    }
}
