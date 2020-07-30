using System;

namespace AspNetCoreWebApplication.Models
{
    public class Card : IEquatable<Card>
    {
        public enum CardSuitValue
        {
            Clubs = 1,
            Diamonds,
            Hearts,
            Spades
        }

        public enum CardRankValue : int
        {
            AceLow = 10,
            Two = 20,
            Three = 30,
            Four = 40,
            Five = 50,
            Six = 60,
            Seven = 70,
            Eight = 80,
            Nine = 90,
            Ten = 100,
            Jack = 110,
            Queen = 120,
            King = 130,
            AceHigh = 140
        }

        private int CardId { get; set; }                        //  id from 1-52
        private int Rank { get; set; }                          //  numeric rank, 1-13
        public CardSuitValue Suit { get; private set; }
        public CardRankValue CardRank { get; private set; }
        public string DisplayName { get; set; }

        public string RankSym { get; set; }             //  A, K, Q, J, T, etc.
        public string SuitSym { get; private set; }             //  c, d, h, s
        public string DisplaySuitSym { get; private set; }
        public string BackSym { get; private set; } = "§";
        public string CardSym { get; private set; }             //  Determines what is seen on board; backsym when face daown, card value when face up,.e.g. 9h, Ad, etc.
        public bool IsFaceDown { get; private set; } = true;    //  Turn face up/down

        public Card(int cardid)
        {
            this.CardId = cardid;
            decimal suitno = (this.CardId.Equals(1) ? 0 : ((this.CardId - 1) / 13));

            //  Determine card Suit
            switch (Math.Round(suitno, 0, MidpointRounding.ToZero))
            {
                case 0:
                    this.Suit = CardSuitValue.Clubs;
                    this.SuitSym = "c"; //"♣";
                    this.DisplaySuitSym = "♣";
                    this.Rank = CardId;
                    break;
                case 1:
                    this.Suit = CardSuitValue.Diamonds;
                    this.SuitSym = "d"; //"♦";
                    this.DisplaySuitSym = "♦";
                    this.Rank = (CardId - 13);
                    break;
                case 2:
                    this.Suit = CardSuitValue.Hearts;
                    this.SuitSym = "h"; //"♥";
                    this.DisplaySuitSym = "♥";
                    this.Rank = (CardId - 26);
                    break;
                case 3:
                    this.Suit = CardSuitValue.Spades;
                    this.SuitSym = "s"; //"♠";
                    this.DisplaySuitSym = "♠";
                    this.Rank = (CardId - 39);
                    break;
            }

            switch (this.Rank)
            {
                case 1:
                    this.DisplayName = "Two of " + this.Suit;
                    this.RankSym = "2";
                    this.CardRank = CardRankValue.Two;
                    break;
                case 2:
                    this.DisplayName = "Three of " + this.Suit;
                    this.RankSym = "3";
                    this.CardRank = CardRankValue.Three;
                    break;
                case 3:
                    this.DisplayName = "Four of " + this.Suit;
                    this.RankSym = "4";
                    this.CardRank = CardRankValue.Four;
                    break;
                case 4:
                    this.DisplayName = "Five of " + this.Suit;
                    this.RankSym = "5";
                    this.CardRank = CardRankValue.Five;
                    break;
                case 5:
                    this.DisplayName = "Six of " + this.Suit;
                    this.RankSym = "6";
                    this.CardRank = CardRankValue.Six;
                    break;
                case 6:
                    this.DisplayName = "Seven of " + this.Suit;
                    this.RankSym = "7";
                    this.CardRank = CardRankValue.Seven;
                    break;
                case 7:
                    this.DisplayName = "Eight of " + this.Suit;
                    this.RankSym = "8";
                    this.CardRank = CardRankValue.Eight;
                    break;
                case 8:
                    this.DisplayName = "Nine of " + this.Suit;
                    this.RankSym = "9";
                    this.CardRank = CardRankValue.Nine;
                    break;
                case 9:
                    this.DisplayName = "Ten of " + this.Suit;
                    this.RankSym = "T";
                    this.CardRank = CardRankValue.Ten;
                    break;
                case 10:
                    this.DisplayName = "Jack of " + this.Suit;
                    this.RankSym = "J";
                    this.CardRank = CardRankValue.Jack;
                    break;
                case 11:
                    this.DisplayName = "Queen of " + this.Suit;
                    this.RankSym = "Q";
                    this.CardRank = CardRankValue.Queen;
                    break;
                case 12:
                    this.DisplayName = "King of " + this.Suit;
                    this.RankSym = "K";
                    this.CardRank = CardRankValue.King;
                    break;
                case 13:
                    this.DisplayName = "Ace of " + this.Suit;
                    this.RankSym = "A";
                    this.CardRank = CardRankValue.AceHigh;
                    break;
            }

            CardSym = BackSym;

        }

        public bool FlipCard()
        {
            if (IsFaceDown)
            {
                IsFaceDown = false;
                CardSym = RankSym + SuitSym;
            }
            else
            {
                IsFaceDown = true;
                CardSym = BackSym;
            }

            return IsFaceDown;
        }

        public void MakeAceLow()
        {
            if (CardRank == CardRankValue.AceHigh)
            {
                this.CardRank = CardRankValue.AceLow;
            }
        }

        public void MakeAceHigh()
        {
            if (CardRank == CardRankValue.AceLow)
            {
                this.CardRank = CardRankValue.AceHigh;
            }
        }

        bool IEquatable<Card>.Equals(Card otherCard)
        {
            if (otherCard == null) return false;
            if (!(otherCard is Card objAsCard)) return false;
            else return Equals(objAsCard);
        }
    }

}
