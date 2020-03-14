namespace EqualsAndGetHashCode_Implementation
{
    using System;
    using EqualsAndGetHashCode;

    public static class EntryPoint
    {
        public static void Main()
        {
            var priceTags = new[]
            {
                new PriceTag(17.6M),
                new PriceTag(17.7M),
                new PriceTag(17.6M),
                new CurrencyPriceTag(17.6M, "USD"),
                new CurrencyPriceTag(17.7M, "USD"),
                new CurrencyPriceTag(17.6M, "GBP"),
                new CurrencyPriceTag(17.6M, "USD"),
                new DiscountedPriceTag(17.6M, 9.2M),
                new DiscountedPriceTag(17.7M, 9.2M),
                new DiscountedPriceTag(17.6M, 9.2M)
            };

            System.Collections.Generic.HashSet<PriceTag> hash =
                new System.Collections.Generic.HashSet<PriceTag>();

            foreach (var tag in priceTags)
            {
                Console.Write("{0,30}", tag);
                if (hash.Contains(tag))
                {
                    Console.WriteLine(" DUPLICATE");
                }
                else
                {
                    Console.WriteLine();
                    hash.Add(tag);
                }
            }

            Console.WriteLine();
            Console.Write("Press ENTER to continue... ");
            Console.ReadLine();
        }
    }
}
