namespace _01._Parking_Zones
{
    using System;

    public class ParkingSpot
    {
        public ParkingSpot(int x, int y, decimal price, ParkingZone zone)
        {
            this.X = x;
            this.Y = y;
            this.Price = price;
            this.ParkingZone = zone;
        }

        public int X { get; }

        public int Y { get; }

        public decimal Price { get; }

        public ParkingZone ParkingZone { get; }

        public override string ToString()
        {
            return $"({this.X}, {this.Y}) {this.Price}";
        }

        public int DistanceToTarget(Point target)
        {
            return (Math.Abs(target.X - this.X) + Math.Abs(target.Y - this.Y) - 1) * 2;
        }

        public int TimeToTargetInMinutes(Point target, int traversalTime)
        {
            var totalTimeInSeconds = this.DistanceToTarget(target) * traversalTime;

            return (int) Math.Ceiling(totalTimeInSeconds / 60M);
        }

        public decimal PriceToTarget(Point target, int traversalTime)
        {
            return this.TimeToTargetInMinutes(target, traversalTime) * this.Price;
        }
    }
}