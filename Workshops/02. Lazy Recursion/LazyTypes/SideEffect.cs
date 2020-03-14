namespace LazyTypes
{
    using System;

    public class SideEffect<T>
    {
        private readonly Func<Lazy<T>> _func;

        public SideEffect(Func<Lazy<T>> func)
        {
            this._func = func;
        }

        public SideEffect<TR> Bind<TR>(Func<Lazy<T>, SideEffect<TR>> secondEffect)
        {
            return new SideEffect<TR>(() => secondEffect(this._func())._func());
        }

        public void Execute()
        {
            this._func();
        }
    }

    public static class SideEffect
    {
        public static SideEffect<int> ReadNumber()
        {
            return new SideEffect<int>(() =>
            {
                var line = Console.ReadLine();
                return new Lazy<int>(() => int.Parse(line));
            });
        }

        public static SideEffect<LazyVoid> PrintNumber(Lazy<int> number)
        {
            return new SideEffect<LazyVoid>(() =>
            {
                Console.WriteLine(number.Value);
                return new Lazy<LazyVoid>(() => LazyVoid.Instance);
            });
        }
    }
}
