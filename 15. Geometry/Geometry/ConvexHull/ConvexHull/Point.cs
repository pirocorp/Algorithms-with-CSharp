namespace ConvexHull
{
    using System;

    public class Point
    {
        private double _x;
        private double _y;

        /// <summary>
        /// Double precision
        /// </summary>
        private const double EPSILON = 1e-12;

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X
        {
            get => this._x;
            set => this._x = value;
        }

        public double Y
        {
            get => this._y;
            set => this._y = value;
        }

        public double DistanceToSquared(Point other)
        {
            var dX = this.X - other.X;
            var dY = this.Y - other.Y;

            return (dX * dX + dY * dY);
        }

        public double DistanceTo(Point other)
        {
            return Math.Sqrt(this.DistanceToSquared(other));
        }

        public static Point GeneratePoint(Random rnd)
        {
            var x = GenerateCoordinate(rnd);
            var y = GenerateCoordinate(rnd);

            return new Point(x, y);
        }

        public static int XComparer(Point a, Point b)
        {
            var dx = a.X - b.X;

            if (dx < -EPSILON)
            {
                return -1;
            }

            if (dx > EPSILON)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }

        private static double GenerateCoordinate(Random rnd)
        {
            //return rnd.Next();
            return rnd.Next() % 1000
                   + (double) (rnd.Next() % 1000) / (rnd.Next() % 1000 + 1);
        }
    }
}
