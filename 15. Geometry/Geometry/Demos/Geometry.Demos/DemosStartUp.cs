namespace Geometry.Demos
{
    using System;
    using System.Collections.Generic;
    using Objects;

    public static class DemosStartUp
    {
        private static readonly List<Point> _points = new List<Point>
        {
            new Point(0, 0),
            new Point(4, 0),
            new Point(0, 3),

            new Point(34, 0),
            new Point(-123.5, -12726),
            new Point(3858, 2323),

            new Point(-123.5, -12726),
            new Point(34, 0),
            new Point(3858, 2323),

            new Point(2, 0),
            new Point(1, 1),
            new Point(0, 2),
        };

        public static void Main()
        {
            for (var i = 0; i < _points.Count; i += 3)
            {
                var a = _points[i];
                var b = _points[i + 1];
                var c = _points[i + 2];

                var triangle = new Triangle(a, b, c);
                Console.WriteLine(triangle.AreaWithHeron());
                Console.WriteLine(triangle.DirectedArea());
                Console.WriteLine(new string('-', Console.WindowWidth));
            }
        }
    }
}
