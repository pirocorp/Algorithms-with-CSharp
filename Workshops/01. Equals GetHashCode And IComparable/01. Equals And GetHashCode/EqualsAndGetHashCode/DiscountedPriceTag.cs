namespace EqualsAndGetHashCode
{
    public class DiscountedPriceTag : PriceTag
    {
        private decimal _prevPrice;

        public DiscountedPriceTag() { }

        public DiscountedPriceTag(decimal price, decimal prevPrice)
            : base(price)
        {
            this.PrevPrice = prevPrice;
        }

        public decimal PrevPrice
        {
            get => this._prevPrice;
            set
            {
                if (value >= 0)
                {
                    this._prevPrice = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return this.EqualsDerived(obj) && obj.GetType() == typeof(DiscountedPriceTag);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(DiscountedPriceTag dp1, DiscountedPriceTag dp2)
        {

            if (object.ReferenceEquals(dp1, dp2))
            {
                return true;
            }

            if (!object.ReferenceEquals(dp1, null) &&
                !object.ReferenceEquals(dp2, null) &&
                dp1.Equals(dp2))
            {
                return true;
            }

            return false;

        }

        public static bool operator !=(DiscountedPriceTag dp1, DiscountedPriceTag dp2)
        {
            return !(dp1 == dp2);
        }

        public override string ToString()
        {
            return $"DiscountedPriceTag {this.Price}";
        }
    }
}