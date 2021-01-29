namespace _01._Compare_And_Swap
{
    using System;
    using System.Collections.Generic;

    public class Example<TIdentifier, TValue>        
    {
        private IDictionary<TIdentifier, TValue> collection;

        public Example()
        {
            this.collection = new Dictionary<TIdentifier, TValue>();
        }

        // An atomic method used in multithreading to achieve synchronization
        public bool CompareAndSwap(
            TIdentifier location,
            TValue oldValue,
            TValue newValue)
        {
            var currentValue = this.collection[location];

            if(currentValue.Equals(oldValue))
            {
                this.collection[location] = newValue;
                return true;
            }

            return false;
        }
    }

    public class Consumer
    {
        public void Conusme()
        {
            var example = new Example<int, string>();

            var oldValue = GetOldValue();

            while (!example.CompareAndSwap(1, oldValue, "new"))
            {
                oldValue = GetOldValue();
            }

            // The value is updated successfully
        }

        public string GetOldValue()
        {
            // Get old value
            throw new NotImplementedException();
        }
    }
}
