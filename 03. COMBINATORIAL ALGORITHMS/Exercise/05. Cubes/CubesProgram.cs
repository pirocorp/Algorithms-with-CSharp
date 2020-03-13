namespace _05._Cubes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CubesProgram
    {
        private static int _totalCubes = 1;
        private static string[] _cubes;
        private static readonly HashSet<string> Rotations = new HashSet<string>();

        public static void Main()
        {
            _cubes = Console.ReadLine().Split().OrderBy(x => x).ToArray();

            Rotate();
            Permute(0, _cubes.Length - 1);

            Console.WriteLine(_totalCubes);
        }

        private static void Rotate()
        {
            for (int z = 0; z < 4; z++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        var cube = string.Join("", _cubes);
                        Rotations.Add(cube);

                        RotateX();
                    }

                    RotateY();
                }

                RotateZ();
            }
        }

        private static void RotateX()
        {
            Swap(10, 0);
            Swap(11, 10);
            Swap(3, 11);

            Swap(9, 4);
            Swap(7, 9);
            Swap(1, 7);

            Swap(5, 2);
            Swap(6, 5);
            Swap(8, 6);
        }

        private static void RotateY()
        {
            Swap(3, 1);
            Swap(7, 3);
            Swap(8, 7);

            Swap(11, 0);
            Swap(6, 11);
            Swap(2, 6);

            Swap(10, 4);
            Swap(9, 10);
            Swap(5, 9);
        }

        private static void RotateZ()
        {
            Swap(6, 9);
            Swap(7, 6);
            Swap(11, 7);

            Swap(5, 10);
            Swap(8, 5);
            Swap(3, 8);

            Swap(2, 4);
            Swap(1, 2);
            Swap(0, 1);
        }

        public static void Permute(int start, int end)
        {
            if (!Rotations.Contains(string.Join("", _cubes)))
            {
                _totalCubes++;
                Rotate();
            }

            for (int left = end - 1; left >= start; left--)
            {
                for (int right = left + 1; right <= end; right++)
                {
                    if (_cubes[left] != _cubes[right])
                    {
                        Swap(left, right);
                        Permute(left + 1, end);
                    }
                }

                var firstElement = _cubes[left];
                for (int i = left; i <= end - 1; i++)
                {
                    _cubes[i] = _cubes[i + 1];
                }

                _cubes[end] = firstElement;
            }
        }

        private static void Swap(int first, int second)
        {
            var temp = _cubes[first];
            _cubes[first] = _cubes[second];
            _cubes[second] = temp;
        }
    }
}
