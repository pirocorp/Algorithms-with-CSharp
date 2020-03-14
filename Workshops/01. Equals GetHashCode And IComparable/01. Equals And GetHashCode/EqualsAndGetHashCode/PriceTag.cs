namespace EqualsAndGetHashCode
{
    public class PriceTag
    {
        private decimal _price;

        public PriceTag()
        {
        }

        public PriceTag(decimal price)
        {
            this.Price = price;
        }

        public decimal Price
        {
            get => this._price;
            private set
            {
                if (value >= 0)
                {
                    this._price = value;
                }
            }
        }

        public override int GetHashCode()
        {
            return this._price.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.EqualsDerived(obj) && obj.GetType() == typeof(PriceTag);
        }

        protected virtual bool EqualsDerived(object obj)
        {
            return !ReferenceEquals(obj, null) &&
                   obj is PriceTag &&
                   ((PriceTag)obj)._price == this._price;
        }

        public static bool operator == (PriceTag pt1, PriceTag pt2)
        {

            if (ReferenceEquals(pt1, pt2))
            {
                return true;
            }

            if (!ReferenceEquals(pt1, null) &&
                !ReferenceEquals(pt2, null) &&
                pt1.Equals(pt2))
            {
                return true;
            }

            return false;
        }

        public static bool operator != (PriceTag pt1, PriceTag pt2)
        {
            return !(pt1 == pt2);
        }

        public override string ToString()
        {
            return $"PriceTag {this._price}";
        }
    }
}
