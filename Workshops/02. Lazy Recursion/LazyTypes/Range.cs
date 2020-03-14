namespace LazyTypes
{
    using System;
    using System.Collections.Generic;

    public static class Range
    {
        public static Lazy<List<int>> FromTo(Lazy<int> begin, Lazy<int> end)
        {
            if (begin.Value >= end.Value)
            {
                return new Lazy<List<int>>(() => new List<int>());
            }

            var nextValue = new Lazy<int>(() => begin.Value + 1);
            var list = new List<int>(begin, FromTo(nextValue, end));
            return new Lazy<List<int>>(() => list);
        }

        public static Lazy<List<int>> From(Lazy<int> begin)
        {
            var nextValue = new Lazy<int>(() => begin.Value + 1);
            var list = new List<int>(begin, From(nextValue));
            return new Lazy<List<int>>(() => list);
        }

        public static Lazy<List<T>> FromIEnumerable<T>(this IEnumerable<T> values)
        {
            return FromIEnumerator(values.GetEnumerator());
        }

        private static Lazy<List<T>> FromIEnumerator<T>(this IEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                return new Lazy<List<T>>(() => new List<T>());
            }

            var head = new Lazy<T>(() => enumerator.Current);
            var list = new List<T>(head, FromIEnumerator(enumerator));
            return new Lazy<List<T>>(() => list);
        }
    }
}
