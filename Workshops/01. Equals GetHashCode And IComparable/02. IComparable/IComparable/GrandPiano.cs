namespace IComparable
{
    using System;

    public class GrandPiano : Piano
    {
        public string Producer { get; set; }

        protected override int CompareToValues(Piano other)
        {
            // Derived class must override this method if it has
            // added fields that affect comparison result.
            // New fields are taken into account only if base class
            // finds that base fields are equal.

            var result = base.CompareToValues(other);

            if (result == 0 && other is GrandPiano)
            {
                var gp = (GrandPiano) other;
                var thisProducer = this.Producer ?? string.Empty;
                var otherProducer = gp.Producer ?? string.Empty;

                result = string.Compare(thisProducer, otherProducer, StringComparison.InvariantCulture);
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format($"Mark={this.Mark:0.0}, Producer={this.Producer}, GrandPiano");
        }

        //Note that derived classes do not have to override Equals method -
        //all the logic has already been provided by the base class. 

        //It is only important for them to keep the GetHashCode method
        //consistent by adding the fingerprint of their own properties.
        public override int GetHashCode()
        {
            var prod = this.Producer ?? string.Empty;
            return base.GetHashCode() ^ prod.GetHashCode();
        }
    }
}