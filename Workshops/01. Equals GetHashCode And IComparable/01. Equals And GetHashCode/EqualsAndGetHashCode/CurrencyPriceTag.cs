namespace EqualsAndGetHashCode
{
    public class CurrencyPriceTag : PriceTag
    {
        private string _currency = "EUR";

        public CurrencyPriceTag() { }

        public CurrencyPriceTag(decimal price, string currency)
            : base(price)
        {
            this.Currency = currency;
        }

        public CurrencyPriceTag(PriceTag pt)
            : base(pt.Price)
        {
        }

        public string Currency
        {
            get => this._currency;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._currency = value;
                }
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this._currency.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.EqualsDerived(obj) && obj.GetType() == typeof(CurrencyPriceTag);
        }

        protected override bool EqualsDerived(object obj)
        {
            return base.EqualsDerived(obj) &&
                   !object.ReferenceEquals(obj, null) &&
                   obj is CurrencyPriceTag &&
                   ((CurrencyPriceTag)obj)._currency == this._currency;
        }

        public override string ToString()
        {
            return $"CurrencyPriceTag {this.Price} {this.Currency}";
        }

        public static bool operator ==(CurrencyPriceTag cp1, CurrencyPriceTag cp2)
        {
            if (object.ReferenceEquals(cp1, cp2))
            {
                return true;
            }

            if (!object.ReferenceEquals(cp1, null) &&
                !object.ReferenceEquals(cp2, null) &&
                cp1.Equals(cp2))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(CurrencyPriceTag cp1, CurrencyPriceTag cp2)
        {
            return !(cp1 == cp2);
        }
    }
}