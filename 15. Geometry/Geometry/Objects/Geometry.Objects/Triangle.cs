namespace Geometry.Objects
{
    using System;

    public class Triangle
    {
        private Point _a;
        private Point _b;
        private Point _c;

        public Triangle(Point a, Point b, Point c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Point A
        {
            get => this._a;
            set => this._a = value;
        }

        public Point B
        {
            get => this._b;
            set => this._b = value;
        }

        public Point C
        {
            get => this._c;
            set => this._c = value;
        }

        public double AreaWithHeron()
        {
            var ab = this.A.DistanceTo(this.B);
            var ac = this.A.DistanceTo(this.C);
            var bc = this.B.DistanceTo(this.C);

            var p = (ab + ac + bc) / 2;

            return Math.Sqrt(p * (p - ab) * (p - ac) * (p - bc));
        }

        /// <summary>
        /// Formula: (Ax(By-Cy) + Bx(Cy-Ay) + Cx(Ay-By)) / 2
        /// </summary>
        /// <remarks>
        /// 
        /// | Ax Ay 1 |
        /// | Bx By 1 |
        /// | Cx Cy 1 |
        /// 
        /// </remarks>
        public double DirectedArea()
        {
            return (  this.A.X * (this.B.Y - this.C.Y) 
                    + this.B.X * (this.C.Y - this.A.Y) 
                    + this.C.X * (this.A.Y - this.B.Y)) / 2;
        }
    }
}
