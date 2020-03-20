namespace _01._Knapsack
{
    public class Item
    {
        public Item(string name, int weight, int price)
        {
            this.Name = name;
            this.Price = price;
            this.Weight = weight;
        }

        public Item(string[]args)
            :this(args[0], int.Parse(args[1]), int.Parse(args[2]))
        {
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ${this.Price} {this.Weight}";
        }
    }
}