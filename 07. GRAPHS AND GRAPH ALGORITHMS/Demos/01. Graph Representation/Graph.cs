namespace _01._Graph_Representation
{
    using System.Collections.Generic;

    public class Graph
    {
        public Graph(List<int>[] childNodes, string[] nodeNames)
        {
            this.ChildNodes = childNodes;
            this.NodeNames = nodeNames;
        }

        public List<int>[] ChildNodes { get; private set; }

        public string[] NodeNames { get; private set; }
    }
}