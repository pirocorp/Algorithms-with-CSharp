namespace LazyTypes
{
    using System;

    public class Optional<T>
    {
        private readonly Lazy<T> _value;

        public Optional()
        {
            this._value = null;
        }

        public Optional(Lazy<T> value)
        {
            this._value = value;
        }

        public Lazy<TR> WithOptional<TR>(Lazy<TR> whenEmpty, Func<Lazy<T>, Lazy<TR>> whenFull)
        {
            if (this._value == null)
            {
                return whenEmpty;
            }

            return whenFull(this._value);
        }

        public Optional<TR> Bind<TR>(Func<Lazy<T>, Optional<TR>> func)
        {
            if (this._value == null)
            {
                return new Optional<TR>();
            }

            return func(this._value);
        }
    }
}
