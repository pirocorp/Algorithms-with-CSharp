namespace _01._Deck_Shuffle
{
    public class Card
    {
        public Card(Face value, Suit suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public Face Value { get; }

        public Suit Suit { get; }

        public override string ToString()
        {
            var value = (int)this.Value <= (int)Face.Ten ? $"{(int)this.Value + 2}" : $"{this.Value}";

            char suit;

            switch (this.Suit)
            {
                case Suit.Club:
                    suit = '\u2663';
                    break;
                case Suit.Diamond:
                    suit = '\u2666';
                    break;
                case Suit.Heart:
                    suit = '\u2665';
                    break;
                case Suit.Spade:
                    suit = '\u2660';
                    break;
                default:
                    suit = ' ';
                    break;
            }

            return $"{value}{suit}";
        }
    }
}
