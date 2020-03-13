//namespace LazyRecursion
//{
//    using System;

//    public class Lazy<T> 
//        where T : struct
//    {
//        private T? _value;
//        private readonly Func<T> _func;

//        public Lazy(Func<T> func)
//        {
//            this._value = null;
//            this._func = func;
//        }

//        public T Value
//        {
//            get
//            {
//                if (!this._value.HasValue)
//                {
//                    this._value = this._func();
//                }

//                return this._value.Value;
//            }
//        }
//    }
//}
