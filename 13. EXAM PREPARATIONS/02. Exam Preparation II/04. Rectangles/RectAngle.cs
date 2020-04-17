namespace _04._Rectangles
{
    using System;

    public class RectAngle
    {
        public RectAngle(string name, int x1, int y1, int x2, int y2)
        {
            this.Name = name;
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }

        public string Name { get; }

        public int X1 { get; }

        public int Y1 { get; }

        public int X2 { get; }

        public int Y2 { get; }

        public int Size => Math.Abs(this.X1 - this.X2) * Math.Abs(this.Y1 - this.Y2);

        public bool isNested(RectAngle other)
        {
            var notNested = (this.X1 > other.X1 ||
                            this.X2 < other.X2 ||
                            this.Y1 < other.Y1 ||
                            this.Y2 > other.Y2);

            return !notNested;
        }

        public override string ToString()
        {
            return $"{this.Name}|({this.X1}, {this.Y1}), ({this.X2}, {this.Y2})";
        }
    }
}
