namespace _01._Parking_Zones
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class ParkingZonesProgram
    {
        private static List<ParkingSpot> _parkingSpots;
        private static Point _targetPoint;
        private static int _blockTraverseTime;

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            var parkingZones = new List<ParkingZone>();
            _parkingSpots = new List<ParkingSpot>();

            for (var i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(":, ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var name = tokens[0];
                var x = int.Parse(tokens[1]);
                var y = int.Parse(tokens[2]);
                var width = int.Parse(tokens[3]);
                var height = int.Parse(tokens[4]);
                var price = decimal.Parse(tokens[5], CultureInfo.InvariantCulture);

                var parkingZone = new ParkingZone(name, x, y, width, height, price);
                parkingZones.Add(parkingZone);
            }

            var inputTokens = Console.ReadLine()
                .Split(", ;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            for (var i = 0; i < inputTokens.Length; i += 2)
            {
                var x = inputTokens[i];
                var y = inputTokens[i + 1];

                foreach (var parkingZone in parkingZones)
                {
                    var parkingSpot = parkingZone.IsInZone(x, y);

                    if (parkingSpot != null)
                    {
                        _parkingSpots.Add(parkingSpot);
                        break;
                    }
                }
            }

            var targetTokens = Console.ReadLine()
                .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            _targetPoint = new Point(targetTokens[0], targetTokens[1]);
            _blockTraverseTime = int.Parse(Console.ReadLine());
        }

        public static void Main()
        {
            ReadInput();

            var result = _parkingSpots
                .OrderBy(x => x.PriceToTarget(_targetPoint, _blockTraverseTime))
                .ThenBy(x => x.DistanceToTarget(_targetPoint))
                .First();

            Console.WriteLine($"Zone Type: {result.ParkingZone.Name}; X: {result.X}; Y: {result.Y}; Price: {result.PriceToTarget(_targetPoint, _blockTraverseTime):F2}");
        }
    }
}
