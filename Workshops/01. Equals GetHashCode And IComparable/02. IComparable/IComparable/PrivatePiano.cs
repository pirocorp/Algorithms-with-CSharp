namespace IComparable
{
    using System;

    public class PrivatePiano : Piano
    {
        public string Owner { get; set; }

        protected override int CompareToValues(Piano other)
        {
            // Derived class must override this method if it has
            // added fields that affect comparison result.
            // New fields are taken into account only if base class
            // finds that base fields are equal.

            var result = base.CompareToValues(other);

            if (result == 0 && other is PrivatePiano)
            {
                var pp = (PrivatePiano) other;
                var thisOwner = this.Owner ?? string.Empty;
                var otherOwner = pp.Owner ?? string.Empty;

                result = string.Compare(thisOwner, otherOwner, StringComparison.InvariantCulture);
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format($"Mark={this.Mark:0.0}, Owner={this.Owner}, GrandPiano");
        }

        //Note that derived classes do not have to override Equals method -
        //all the logic has already been provided by the base class. 

        //It is only important for them to keep the GetHashCode method
        //consistent by adding the fingerprint of their own properties.
        public override int GetHashCode()
        {
            var owner = this.Owner ?? string.Empty;
            return base.GetHashCode() ^ owner.GetHashCode();
        }
    }
}