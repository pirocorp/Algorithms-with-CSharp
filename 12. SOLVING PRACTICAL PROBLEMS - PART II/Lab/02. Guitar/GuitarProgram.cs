namespace _02._Guitar
{
    using System;
    using System.Linq;

    public static class GuitarProgram
    {
        private static bool[,] _volumes;
        private static int[] _elements;
        private static int _maxVolume;

        private static void ReadInput()
        {
            _elements = Console.ReadLine()
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var initialVolume = int.Parse(Console.ReadLine());
            _maxVolume = int.Parse(Console.ReadLine());

            _volumes = new bool[_elements.Length + 1, _maxVolume + 1];
            _volumes[0, initialVolume] = true;
        }

        private static void CalculateVolumes()
        {
            for (var row = 1; row <= _elements.Length; row++)
            {
                var element = _elements[row - 1];
                var isFound = false;

                for (var col = 0; col <= _maxVolume; col++)
                {
                    if (!_volumes[row - 1, col])
                    {
                        continue;
                    }

                    isFound = true;

                    var addedVolume = col + element;
                    var subtractedVolume = col - element;

                    if (addedVolume <= _maxVolume)
                    {
                        _volumes[row, addedVolume] = true;
                    }

                    if (subtractedVolume >= 0)
                    {
                        _volumes[row, subtractedVolume] = true;
                    }
                }

                if (!isFound)
                {
                    return;
                }
            }
        }

        private static void ReconstructResult()
        {
            var result = -1;

            for (var i = _maxVolume; i >= 0; i--)
            {
                if (_volumes[_elements.Length, i])
                {
                    result = i;
                    break;
                }
            }

            Console.WriteLine(result);
        }

        public static void Main()
        {
            ReadInput();

            CalculateVolumes();

            ReconstructResult();
        }
    }
}
