namespace ConvexHull
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static void RemoveLast<T>(this List<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
    }
}
