namespace _03._Towns
{
    using System;
    using System.Linq;

    public static class TownsProgram
    {
        private static void CalculateLis(int[] sequence, int[] lengths)
        {
            for (var current = 0; current < sequence.Length; current++)
            {
                var maxLength = 1;
                var prevIndex = -1;
                var currentNumber = sequence[current];

                for (var prev = 0; prev < current; prev++)
                {
                    var prevNumber = sequence[prev];
                    var prevSolutionLength = lengths[prev];

                    if (currentNumber > prevNumber &&
                        maxLength <= prevSolutionLength)
                    {
                        maxLength = prevSolutionLength + 1;
                        prevIndex = prev;
                    }
                }

                lengths[current] = maxLength;
            }
        }

        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var seq = new int[n];

            for (var i = 0; i < n; i++)
            {
                var number = int.Parse(Console.ReadLine()
                    .Split(' ')[0]);

                seq[i] = number;
            }

            var lenLis = new int[seq.Length];

            CalculateLis(seq, lenLis);

            var reverse = seq.Reverse().ToArray();
            var lenLds = new int[reverse.Length];

            CalculateLis(reverse, lenLds);

            lenLds = lenLds.Reverse().ToArray();

            var maxLengths = new int[lenLds.Length];

            for (var i = 0; i < lenLds.Length; i++)
            {
                maxLengths[i] = lenLds[i] + lenLis[i];
            }

            Console.WriteLine(maxLengths.Max() - 1);
        }
    }
}
