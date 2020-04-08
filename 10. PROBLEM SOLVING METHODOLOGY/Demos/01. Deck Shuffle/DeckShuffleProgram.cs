namespace _01._Deck_Shuffle
{
    using System;
    using System.Collections.Generic;

    public static class DeckShuffleProgram
    {
        private static Random _random;

        private static List<Card> CreateDeckOfCards()
        {
            var deck = new List<Card>
            {
                new Card(Face.Two, Suit.Club),
                new Card(Face.Three, Suit.Club),
                new Card(Face.Four, Suit.Club),
                new Card(Face.Five, Suit.Club),
                new Card(Face.Six, Suit.Club),
                new Card(Face.Seven, Suit.Club),
                new Card(Face.Eight, Suit.Club),
                new Card(Face.Nine, Suit.Club),
                new Card(Face.Ten, Suit.Club),
                new Card(Face.J, Suit.Club),
                new Card(Face.Q, Suit.Club),
                new Card(Face.K, Suit.Club),
                new Card(Face.A, Suit.Club),
                new Card(Face.Two, Suit.Diamond),
                new Card(Face.Three, Suit.Diamond),
                new Card(Face.Four, Suit.Diamond),
                new Card(Face.Five, Suit.Diamond),
                new Card(Face.Six, Suit.Diamond),
                new Card(Face.Seven, Suit.Diamond),
                new Card(Face.Eight, Suit.Diamond),
                new Card(Face.Nine, Suit.Diamond),
                new Card(Face.Ten, Suit.Diamond),
                new Card(Face.J, Suit.Diamond),
                new Card(Face.Q, Suit.Diamond),
                new Card(Face.K, Suit.Diamond),
                new Card(Face.A, Suit.Diamond),
                new Card(Face.Two, Suit.Heart),
                new Card(Face.Three, Suit.Heart),
                new Card(Face.Four, Suit.Heart),
                new Card(Face.Five, Suit.Heart),
                new Card(Face.Six, Suit.Heart),
                new Card(Face.Seven, Suit.Heart),
                new Card(Face.Eight, Suit.Heart),
                new Card(Face.Nine, Suit.Heart),
                new Card(Face.Ten, Suit.Heart),
                new Card(Face.J, Suit.Heart),
                new Card(Face.Q, Suit.Heart),
                new Card(Face.K, Suit.Heart),
                new Card(Face.A, Suit.Heart),
                new Card(Face.Two, Suit.Spade),
                new Card(Face.Three, Suit.Spade),
                new Card(Face.Four, Suit.Spade),
                new Card(Face.Five, Suit.Spade),
                new Card(Face.Six, Suit.Spade),
                new Card(Face.Seven, Suit.Spade),
                new Card(Face.Eight, Suit.Spade),
                new Card(Face.Nine, Suit.Spade),
                new Card(Face.Ten, Suit.Spade),
                new Card(Face.J, Suit.Spade),
                new Card(Face.Q, Suit.Spade),
                new Card(Face.K, Suit.Spade),
                new Card(Face.A, Suit.Spade),
            };

            return deck;
        }

        private static void PrintDeck(IList<Card> deck)
        {
            for (var i = 0; i < deck.Count; i++)
            {
                var card = deck[i];

                if (i % 13 == 0)
                {
                    Console.WriteLine();
                }

                Console.Write(card + " ");
            }

            Console.WriteLine();
        }

        private static void Exchange(IList<Card> deck, int index)
        {
            var randomIndex = index;

            while (randomIndex == index)
            {
                randomIndex = _random.Next(0, deck.Count);
            }

            var card = deck[index];
            deck[index] = deck[randomIndex];
            deck[randomIndex] = card;
        }

        private static void Shuffle(IList<Card> deck)
        {
            if (deck.Count <= 1)
            {
                return;
            }

            if (deck.Count == 2)
            {
                Exchange(deck, 0);
                return;
            }

            for (var i = 0; i < deck.Count; i++)
            {
                Exchange(deck, i);
            }
        }

        public static void Main()
        {
            _random = new Random();
            var deck = CreateDeckOfCards();

            deck = new List<Card>
            {
                new Card(Face.Four, Suit.Diamond),
                new Card(Face.Eight, Suit.Heart)
            };

            PrintDeck(deck);

            Shuffle(deck);

            PrintDeck(deck);
        }
    }
}