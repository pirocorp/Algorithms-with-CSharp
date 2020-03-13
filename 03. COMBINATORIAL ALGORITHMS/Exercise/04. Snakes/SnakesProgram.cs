namespace _04._Snakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SnakesProgram
    {
        private static int n;
        private static readonly HashSet<string> _visited = new HashSet<string>();
        private static readonly HashSet<string> _snakes = new HashSet<string>();
        private static readonly HashSet<string> _distinctSnakes = new HashSet<string>();

        private static char[] _currentSnake;

        public static void Main()
        {
            n = int.Parse(Console.ReadLine());
            _currentSnake = new char[n];

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            GenerateSnakes(0, 0, 0, 'S');
            Console.WriteLine(string.Join(Environment.NewLine, _distinctSnakes));
            Console.WriteLine($"Snakes count = {_distinctSnakes.Count}");

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed);
        }

        private static void GenerateSnakes(int index, int row, int col, char direction)
        {
            if (index == n - 1)
            {
                var currentCell = $"{row} {col}";

                _currentSnake[index] = direction;
                var currentSnake = new string(_currentSnake);

                if (!_snakes.Contains(currentSnake) &&
                    !_visited.Contains(currentCell))
                {
                    _distinctSnakes.Add(currentSnake);

                    _snakes.Add(currentSnake);
                    GenerateSimilarSnakes(_currentSnake);
                }
            }
            else
            {
                var currentCell = $"{row} {col}";

                if (!_visited.Contains(currentCell))
                {
                    _visited.Add(currentCell);
                    _currentSnake[index] = direction;

                    GenerateSnakes(index + 1, row, col + 1, 'R');
                    GenerateSnakes(index + 1, row + 1, col, 'D');
                    GenerateSnakes(index + 1, row, col - 1, 'L');
                    GenerateSnakes(index + 1, row - 1, col, 'U');

                    _visited.Remove(currentCell);
                }
            }
        }

        private static void GenerateSimilarSnakes(char[] currentSnake)
        {
            var current = new string(currentSnake);
            var flipped = FlipSnake(current);
            var reversedCurrent = ReverseSnake(current);
            var reversedFlipped = ReverseSnake(flipped);

            GenerateRotations(current);
            GenerateRotations(flipped);
            GenerateRotations(reversedCurrent);
            GenerateRotations(reversedFlipped);
        }

        private static string ReverseSnake(string snake)
        {
            var reversed = snake.Skip(1).Reverse()
                .ToList();

            reversed.Insert(0, snake[0]);

            return new string(reversed.ToArray());
        }

        private static string FlipSnake(string snake)
        {
            var currentSnake = snake.ToCharArray();

            //Flip by X
            for (var i = 0; i < currentSnake.Length; i++)
            {
                if (currentSnake[i] == 'R')
                {
                    currentSnake[i] = 'L';
                }
                else if (currentSnake[i] == 'L')
                {
                    currentSnake[i] = 'R';
                }
            }

            return new string(currentSnake);
        }

        private static void GenerateRotations(string currentSnake)
        {
            _snakes.Add(currentSnake);

            var snake = currentSnake.ToCharArray();

            for (var i = 0; i < 3; i++)
            {
                currentSnake = Rotate(snake);
                _snakes.Add(currentSnake);
            }
        }

        private static string Rotate(char[] snake)
        {
            for (var i = 1; i < snake.Length; i++)
            {
                switch (snake[i])
                {
                    case 'R': snake[i] = 'D'; break;
                    case 'D': snake[i] = 'L'; break;
                    case 'L': snake[i] = 'U'; break;
                    case 'U': snake[i] = 'R'; break;
                    default: break;
                }
            }

            return new string(snake);
        }
    }
}
