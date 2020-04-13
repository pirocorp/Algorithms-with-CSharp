namespace _01._Parking_Zones
{
    public class ParkingZone
    {
        private readonly string _name;
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;
        private readonly decimal _price;

        public ParkingZone(string name, int x, int y, int width, int height, decimal price)
        {
            this._name = name;
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            this._price = price;
        }

        public string Name => this._name;

        public ParkingSpot IsInZone(int x, int y)
        {
            if (x >= this._x && x < this._x + this._width
                && y >= this._y && y < this._y + this._height)
            {
                return new ParkingSpot(x, y, this._price, this);
            }

            return null;
        }
    }
}
