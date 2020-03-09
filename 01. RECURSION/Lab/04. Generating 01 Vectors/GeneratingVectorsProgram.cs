namespace _04._Generating_01_Vectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GeneratingVectorsProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var result = GetVectors(n)
                .Select(x => string.Join("", x));

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static List<byte[]> GetVectors(int i)
        {
            var vectors = new List<byte[]>();

            GenerateVectors(new byte[i], 0, vectors);

            return vectors;
        }

        private static void GenerateVectors(byte[] vector, int index, List<byte[]> vectors)
        {
            if (index == vector.Length)
            {
                var clone = new byte[vector.Length];
                Array.Copy(vector, clone, vector.Length);
                vectors.Add(clone);
                return;
            }

            for (byte i = 0; i <= 1; i++)
            {
                vector[index] = i;
                GenerateVectors(vector, index + 1, vectors);
            }
        }
    }
}
