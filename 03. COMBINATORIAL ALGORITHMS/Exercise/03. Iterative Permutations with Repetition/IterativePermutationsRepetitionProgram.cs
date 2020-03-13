namespace _03._Iterative_Permutations_with_Repetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IterativePermutationsRepetitionProgram
    {
        public static void Main()
        {
            var result = GenAllPerms(4, 3);

            foreach (var el in result)
            {
                Console.WriteLine(string.Join(" ", el));
            }
        }

        private static List<List<int>> GenAllPerms(int n, int m)
        {
            var perms = new List<List<int>>();
            var perm = new List<int>();

            var minVal = 0;
            var maxVal = m;    // (m+1) different values allowed

            while (true)
            {
                if (perm.Count == n)
                {
                    perms.Add(Clone(perm.ToArray()));
                    while (perm.Count != 0 && perm[^1] == maxVal)
                    {
                        perm.RemoveAt(perm.Count - 1);
                    }

                    if (perm.Count == 0)
                    {
                        break;
                    }
                    perm[^1] += 1;
                }
                else
                {
                    perm.Add(minVal);
                }
            }
            return perms;
        }

        private static List<int> Clone(int[] source)
        {
            var size = source.Length;
            var clone = new int[size];
            Array.Copy(source, clone, size);
            return clone.ToList();
        }
    }

    
}
