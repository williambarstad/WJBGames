using System;
using System.Collections;
using System.Collections.Generic;

namespace WJBPoker.Models
{
    public class Deck : IEnumerable<Card>
    {
        private static List<Card> Cards = new List<Card>();
        private Random rng = new Random();
        private static int NextCard = 0;
        private static bool AllowPeek = true;

        public Deck()
        {
            BuildDeck();
        }

        private void BuildDeck()
        {
            //  Clear the Deck
            Cards.Clear();

            //  Build the deck
            for (int i = 1; i <= 52; i++)
            {
                try
                {
                    var newCard = new Card(i);
                    Cards.Add(newCard);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }

            ResetDeck();
        }

        //  TODO: Increase to shuffle 7 times
        public void Shuffle()
        {
            int shuffles = 7;

            for (int i = 0; i < (shuffles - 1); i++)
            {
                int n = Cards.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Card value = Cards[k];
                    Cards[k] = Cards[n];
                    Cards[n] = value;
                }

                Cards.Reverse();
            }

            ResetDeck();
        }

        public Card Deal(bool faceUp = false)
        {
            Card TopCard = Cards[NextCard];
            NextCard++;
            if (faceUp) TopCard.FlipCard();
            return TopCard;
        }

        public void ResetDeck()
        {
            NextCard = 0;
        }

        public void RebuildDeck()
        {
            BuildDeck();
        }

        public void FanDeck()
        {
            Console.WriteLine("Fanning the deck...");

            for (int i = 0; i < Cards.Count; i++)
            {
                Console.WriteLine(Cards[i].DisplayName);
            }

            Console.WriteLine("Card Count: " + Cards.Count.ToString());
            Console.WriteLine(" ");
        }

        public int CardCount()
        {
            return Cards.Count;
        }

        public string Peek()
        {
            string retString;
            if (!AllowPeek)
            {
                retString = "Not your business.";
            }
            else
            {
                retString = Cards[NextCard].DisplayName;
            }

            return retString;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }
    }
}
