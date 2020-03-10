namespace _06._Connected_Areas_in_Matrix
{
    using System;

    public class Area : IComparable<Area>
    {
        public Area(int positionX, int positionY, int size)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Size = size;
        }

        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public int Size { get; private set; }

        public int CompareTo(Area other)
        {
            var cmp = -this.Size.CompareTo(other.Size);

            if (cmp == 0)
            {
                cmp = this.PositionY.CompareTo(other.PositionY);
            }

            if (cmp == 0)
            {
                cmp = this.PositionX.CompareTo(other.PositionX);
            }

            return cmp;
        }
    }
}
