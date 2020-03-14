namespace LazyTypes
{
    using System;

    public class List<T>
    {
        private readonly Optional<Pair<T, List<T>>> _head;

        public List()
        {
            this._head = new Optional<Pair<T, List<T>>>();
        }

        public List(Lazy<T> headValue, Lazy<List<T>> tailList)
        {
            this._head = new Optional<Pair<T, List<T>>>(
               new Lazy<Pair<T, List<T>>>(
                   () => new Pair<T, List<T>>(headValue, tailList)));
        }

        public Lazy<TR> WithList<TR>(Lazy<TR> whenEmpty, Func<Lazy<T>, Lazy<List<T>>, Lazy<TR>> func)
        {
            return this._head.WithOptional(whenEmpty, 
                pair => pair.Value.WithPair(func));
        }
    }
}
