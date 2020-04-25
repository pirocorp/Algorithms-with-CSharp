namespace _02._Multiple_Patterns_String_Searching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Trie
    {
        private readonly IDictionary<char, Trie> _children;
        private string _pattern;
        private Trie _failLink;
        private Trie _successLink;

        public Trie()
        {
            this._children = new Dictionary<char, Trie>();
            this._pattern = null;
            this._failLink = null;
            this._successLink = null;
        }

        public void Precompute()
        {
            var queue = new Queue<Trie>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                var successNode = node._failLink;
                while (successNode != null && successNode._pattern == null)
                {
                    successNode = successNode._failLink;
                }

                node._successLink = successNode;
                
                foreach (var child in node._children)
                {
                    var failNode = node._failLink;

                    while (failNode != null 
                    && !failNode._children.ContainsKey(child.Key))
                    {
                        failNode = failNode._failLink;
                    }

                    child.Value._failLink = failNode == null 
                        ? this
                        : failNode._children[child.Key];

                    queue.Enqueue(child.Value);
                }
            }
        }

        public void AddString(string str)
        {
            var currentNode = this;

            foreach (var character in str)
            {
                if (!currentNode._children.ContainsKey(character))
                {
                    currentNode._children[character] = new Trie();
                }

                currentNode = currentNode._children[character];
            }

            currentNode._pattern = str;
        }

        public void AhoCorasick(string text)
        {
            var currentNode = this;

            for (var i = 0; i < text.Length; i++)
            {
                while (currentNode != null && !currentNode._children.ContainsKey(text[i]))
                {
                    currentNode = currentNode._failLink;
                }

                currentNode = currentNode == null
                    ? this
                    : currentNode._children[text[i]];


                if (currentNode._pattern != null)
                {
                    PrintMatch(i + 1 - currentNode._pattern.Length, currentNode._pattern);

                }

                var successNode = currentNode._successLink;

                while (successNode != null)
                {
                    PrintMatch(i + 1 - successNode._pattern.Length, successNode._pattern);
                    successNode = successNode._successLink;
                }
            }
        }

        public void Dfs()
        {
            this.Dfs("");
        }

        private void Dfs(string str)
        {
            Console.WriteLine($"{str} -> {this._failLink?.PrintMe()}");

            foreach (var child in this._children)
            {
                child.Value.Dfs(str + child.Key);
            }
        }

        private string PrintMe(int count = 0)
        {
            if (this._pattern != null)
            {
                return this._pattern.Substring(0, this._pattern.Length - count);
            }

            return this._children.First().Value.PrintMe(count + 1);
        }

        private static void PrintMatch(int index, string pattern)
        {
            Console.WriteLine($"{new string(' ', index)}{pattern}");
        }
    }
}
