namespace Geometry.Objects
{
    public class ConsistentRandom
    {
        /// <summary>
        /// Prime numbers
        /// </summary>
        private const int MULTIPLIER = 359;
        private const int ADDER = 10_037;
        private const int MODULO = 1_000_000_007;

        private long _current;

        public ConsistentRandom()
        {
            //Will always start from 42
            this._current = 42;
        }

        public int Next()
        {
            this._current = (this._current * MULTIPLIER + ADDER) % MODULO;
            return (int)this._current;
        }
    }
}
