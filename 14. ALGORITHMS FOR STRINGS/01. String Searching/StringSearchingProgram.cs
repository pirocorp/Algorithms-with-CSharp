namespace _01._String_Searching
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public static class StringSearchingProgram
    {
        private static StringBuilder _results;
        private static long _count;

        private static void PrintMatch(int index, string pattern)
        {
            _results.AppendLine($"{new string(' ', index)}{pattern}");
            _count++;
        }

        private static void AlgorithmRunner(string text, Stopwatch stopWatch, string pattern, Action<string, string> algorithm)
        {
            _results = new StringBuilder();
            _count = 0;

            Console.WriteLine($"{algorithm.Method.Name} Algorithm");
            Console.WriteLine($"Searching: {pattern}");
            Console.WriteLine(text);
            stopWatch.Start();
            algorithm(pattern, text);
            stopWatch.Stop();
            Console.WriteLine(_results);
            Console.WriteLine(_count);
            Console.WriteLine(stopWatch.Elapsed);
            Console.WriteLine(new string('-', Console.WindowWidth));
            stopWatch.Reset();
        }

        private static string TextGenerator()
        {
            var sb = new StringBuilder();

            var strings = new string[]
            {
                "pesho", "gosho", "agrees massive aid package of immediate support for member states",
                "has agreed an aid package of more than half a trillion euros to provide immediate support for member states, whose economies have been ravaged by the coronavirus outbreak",
                "endorsed the agreements on three important safety nets for workers, businesses and sovereigns, amounting to 540 billion euros",
                "also agreed to work toward a recovery fund which is needed and urgent",
                "longer term recovery plan has also been discussed, but the",
                "struggling to come to an agreement over debt distribution, with northern",
                "and country-wide lockdowns have hit economies hard",
                "business only slowly starting to open in some countries, the urgent need for funds in hard-hit countries like ",
                "whole endeavour is about protecting the integrity of our single market and our union, and if we do it well, and succeed, then the investments will have been worth every single cent we spend on them now",

            };

            var random = new Random();

            for (var i = 0; i < 1_000_000; i++)
            {
                sb.Append(strings[random.Next(strings.Length)]);
            }

            return sb.ToString();
        }

        private static string WorstCaseTextGenerator()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < 3_000_000; i++)
            {
                sb.Append($"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }

            sb.Append("ax");
            return sb.ToString();
        }

        private static void BruteForce(string pattern, string text)
        {
            for (var textIndex = 0; textIndex + pattern.Length <= text.Length; textIndex++)
            {
                var match = true;

                for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
                {
                    if (pattern[patternIndex] != text[textIndex + patternIndex])
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    PrintMatch(textIndex, pattern);
                }
            }
        }

        private static void RabinKarp(string pattern, string text)
        {
            var numberBase = 29;
            var mod = 1_000_000_007L;

            var patternHash = new Hash(numberBase, mod, pattern);
            var windowHash = new Hash(numberBase, mod, text, pattern.Length);

            if (patternHash.Equals(windowHash))
            {
                //TODO Manual check if pattern is equal to substring
                //For resolving possible collisions
                PrintMatch(0, pattern);
            }

            for (var i = 0; i < text.Length - pattern.Length; i++)
            {
                windowHash.Roll(text[i + pattern.Length], text[i]);

                if (patternHash.Equals(windowHash))
                {
                    //TODO Manual check if pattern is equal to substring
                    //For resolving possible collisions
                    PrintMatch(i + 1, pattern);
                }
            }
        }

        private static int[] PreComputeKmp(string pattern)
        {
            var failLink = new int[pattern.Length + 1];

            failLink[0] = -1;
            failLink[1] = 0;

            for (var i = 1; i < pattern.Length; i++)
            {
                var currentFailLink = failLink[i];

                while (currentFailLink >= 0 && pattern[i] != pattern[currentFailLink])
                {
                    currentFailLink = failLink[currentFailLink];
                }

                failLink[i + 1] = currentFailLink + 1;
            }

            return failLink;
        }

        private static void KnuthMorrisPratt(string pattern, string text)
        {
            var failLink = PreComputeKmp(pattern);

            var patternIndex = 0;

            for (var textIndex = 0; textIndex < text.Length; textIndex++)
            {
                while (patternIndex >= 0 && pattern[patternIndex] != text[textIndex])
                {
                    patternIndex = failLink[patternIndex];
                }

                patternIndex++;

                if (patternIndex == pattern.Length)
                {
                    PrintMatch(textIndex - patternIndex + 1, pattern);
                    patternIndex = failLink[patternIndex];
                }
            }
        }

        public static void Main()
        {
            //var text = TextGenerator();

            var patterns = new string[]
            {
                //"aaaaaaaaaax",
                //"pesho",
                //"alabala",
                //"os",
                //"penka",
                //"alabala",
            };

            var texts = new string[]
            {
                //"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaax",
                //"gosho e mnogo po maluk ot pesho pesho",
                //"xalabalabala",
                //"goshotoshopeshotos",
                //"na penka gradinata",
                //"xalalalalabalababalabala",
            };

            var stopWatch = new Stopwatch();

            for (var i = 0; i < patterns.Length; i++)
            {
                var pattern = patterns[i];
                var text = /*WorstCaseTextGenerator()*/texts[i];

                AlgorithmRunner(text, stopWatch, pattern, BruteForce);
                AlgorithmRunner(text, stopWatch, pattern, RabinKarp);
                AlgorithmRunner(text, stopWatch, pattern, KnuthMorrisPratt);

                Console.WriteLine(new string('=', Console.WindowWidth));
                Console.WriteLine(new string('=', Console.WindowWidth));
            }
        }
    }
}
