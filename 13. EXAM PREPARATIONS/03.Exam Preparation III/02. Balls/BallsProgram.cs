namespace _02._Balls
{
    using System;
    using System.Text;

    public static class BallsProgram
    {
        private static int[] _result;
        private static int _pockets;
        private static int _capacity;

        private static readonly StringBuilder _builder = new StringBuilder();

        private static void Generate(int index, int ballsLeft)
        {
            if (index == _pockets)
            {
                if (ballsLeft == 0)
                {
                    _builder.AppendLine(string.Join(", ", _result));
                }

                return;
            }

            var ballsToPut = ballsLeft - (_pockets - (index + 1));

            if (ballsToPut > _capacity)
            {
                ballsToPut = _capacity;
            }

            for (var i = ballsToPut; i > 0; i--)
            {
                if (ballsLeft - i > 0 && index == _pockets)
                {
                    break;
                }

                _result[index] = i;
                Generate(index + 1, ballsLeft - i);
            }
        }

        public static void Main()
        {
            _pockets = int.Parse(Console.ReadLine());
            var balls = int.Parse(Console.ReadLine());
            _capacity = int.Parse(Console.ReadLine());

            _result = new int[_pockets];

            Generate(0, balls);

            Console.WriteLine(_builder.ToString().Trim());
        }
    }
}
