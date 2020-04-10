namespace _03._Lumber
{
    using System;

    public class Log
    {
        private readonly int _ax;
        private readonly int _ay;
        private readonly int _bx;
        private readonly int _by;

        public Log(int id, int ax, int ay, int bx, int by)
        {
            this.Id = id;
            this._ax = ax;
            this._ay = ay;
            this._bx = bx;
            this._by = by;
        }

        public Log(int id, params int[] coordinates) 
            : this(id, coordinates[0], coordinates[1], coordinates[2], coordinates[3])
        { }

        public int Id { get; }

        public int X => this._ax;

        public int Y => this._ay;

        public int Width => Math.Abs(this._ax - this._bx);

        public int Height => Math.Abs(this._ay - this._by);

        public bool IntersectsWith(Log rect) =>
            (rect.X <= this.X + this.Width) && (this.X <= rect.X + rect.Width) &&
            (rect.Y >= this.Y - this.Height) && (this.Y >= rect.Y - rect.Height);

        public bool Intersect(Log other)
        {
            var horizontal = this._ax <= other._bx && this._bx >= other._ax;
            var vertical = this._ay >= other._by && this._by <= other._ay;

            return horizontal && vertical;
        }

        public override string ToString()
        {
            return $"{this.Id}: {{({this.X}, {this.Y}) W: {this.Width}, H: {this.Height}}}";
        }
    }
}
