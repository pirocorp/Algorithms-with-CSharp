namespace _02._Fisher_Yates_Shuffle_Algorithm
{
    using System;
    using System.Collections.Generic;

    public static class ShuffleAlgorithmProgram
    {
        public static void Main(string[] args)
        {
            var collection = new List<int>();

            for (var i = 1; i <= 30; i++)
            {
                collection.Add(i);
            }

            Shuffle(collection);

            Console.WriteLine(string.Join(", ", collection));
        }

        public static void Shuffle<T>(IList<T> collection)
        {
            var random = new Random();
            var currentLength = collection.Count;

            //While there remain elements to shuffle
            while (currentLength > 0)
            {
                // Pick a remaining element
                var index = random.Next(currentLength--);

                // And swap it with the current element
                Swap(collection, currentLength, index);
            }
        }

        private static void Swap<T>(IList<T> collection, int from, int to)
        {
            var swap = collection[from];
            collection[from] = collection[to];
            collection[to] = swap;
        }
    }
}
