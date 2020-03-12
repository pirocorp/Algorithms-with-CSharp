namespace _01._Permutations
{
    using System;
    using System.Linq;

    public static class PermutationsProgram
    {
        public static void Main()
        {
            var elements = Console.ReadLine()
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            CrazyOop(elements);
        }

        private static void CrazyOop(string[] input)
        {
            var permutations = new Permutation<string>(input);
            var perms = permutations.GetPermutationsWithRepetition()
                .Select(x => string.Join(" ", x))
                .ToArray();

            foreach (var perm in perms)
            {
                Console.WriteLine(perm);
            }
        }
    }
}
