namespace LazyTypes
{
    using System;

    public class Pair<T1, T2>
    {
        private readonly Lazy<T1> _first;
        private readonly Lazy<T2> _second;

        public Pair(Lazy<T1> first, Lazy<T2> second)
        {
            this._first = first;
            this._second = second;
        }

        public Lazy<TR> WithPair<TR>(Func<Lazy<T1>, Lazy<T2>, Lazy<TR>> func)
        {
            return func(this._first, this._second);
        }
    }

    public static class Pair
    {
        public static Pair<T1, T2> Make<T1, T2>(Lazy<T1> first, Lazy<T2> second)
        {
            return new Pair<T1, T2>(first, second);
        }
    }
}
