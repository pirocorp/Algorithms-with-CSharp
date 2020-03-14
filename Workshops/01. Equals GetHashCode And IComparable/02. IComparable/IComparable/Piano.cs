namespace IComparable
{
    using System;

    public class Piano : IComparable<Piano>, IComparable
    {
        public float Mark { get; set; }
 
        public int CompareTo(Piano other)
        {
            var result = this.CompareToValues(other);

            // If comparison based solely on values
            // returns zero, indicating that two instances
            // are equal in those fields they have in common,
            // only then we break the tie by comparing
            // data types of the two instances.
            if (result == 0)
            {
                result = this.CompareTypes(other);
            }

            return result;
        }

        protected virtual int CompareToValues(Piano other)
        {
            if(ReferenceEquals(other, null))
            {
                // All instances are greater than null
                return 1;
            }

            // Base class simply compares Mark properties
            return this.Mark.CompareTo(other.Mark);
        }

        protected virtual int CompareTypes(Piano other)
        {
            // Base type is considered less than derived type
            // when two instances have the same values of
            // base fields.

            // Instances of two distinct derived types are
            // ordered by comparing full names of their
            // types when base fields are equal.
            // This is consistent comparison rule for all
            // instances of the two derived types.

            var result = 0;

            var thisType = this.GetType();
            var otherType = other.GetType();

            if (otherType.IsSubclassOf(thisType))
            {
                //other is subclass(derived) of this class
                result = -1;
            }
            else if (thisType.IsSubclassOf(otherType))
            {
                //this is subclass(derived) of other class
                result = 1;     
            }
            else if (thisType != otherType)
            {
                result = string.Compare(thisType.FullName, otherType.FullName, StringComparison.InvariantCulture);
            }

            // cut the tie with a test that returns
            // the same value for all objects
            return result;
        }

        //General IComparable Interface
        //we throwing an exception as soon as CompareTo(Object) method smells
        //a non-piano object.
        public int CompareTo(object obj)
        {
            if (obj != null && !(obj is Piano))
            {
                throw new ArgumentException("Object must be of type Piano.");
            }

            return this.CompareTo(obj as Piano);
        }

        public override string ToString()
        {
            return string.Format($"Mark={this.Mark:0.0}, Piano");
        }

        //When a class implements IComparable<T>, it should also override
        //Equals method in order to be consistent. 
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj as Piano) == 0;
        }

        public override int GetHashCode()
        {
            return this.Mark.GetHashCode();
        }
    }
}
