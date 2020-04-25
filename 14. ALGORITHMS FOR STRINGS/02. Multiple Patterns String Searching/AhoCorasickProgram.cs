namespace _02._Multiple_Patterns_String_Searching
{
    using System;

    //Aho - Corasick
    public static class AhoCorasickProgram
    {
        public static void Main()
        {
            var strings = new string[]
            {
                "a",
                "aa",
                "aaa",
                "aaaa",
            };

            var root = new Trie();

            for (var index = 0; index < strings.Length; index++)
            {
                var str = strings[index];
                root.AddString(str);
            }

            root.Precompute();

            var text = "aaaaa";
            Console.WriteLine(text);

            root.AhoCorasick(text);
        }
    }
}
