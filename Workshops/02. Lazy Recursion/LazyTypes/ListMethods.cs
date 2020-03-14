namespace LazyTypes
{
    using System;

    public static class ListMethods
    {
        public static Lazy<int> Length<T>(this List<T> list)
        {
            return list.WithList(
                new Lazy<int>(() => 0), 
                (head, tail) => new Lazy<int>(() => 1 + tail.Value.Length().Value));
        }
    }
}
