using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class HandTests
    {
        public static Table _table = new Table();
        private Deck _deck = new Deck();
        private Hand _hand = new Hand(_table);
        private int _handSize = 2;

        [TestMethod()]
        public void Hand_CreationTest()
        {
            Assert.IsNotNull(_hand);
        }

        [TestMethod()]
        public void Hand_EnumTest()
        {
            int aFlush = 7000;
            Assert.AreEqual(aFlush, (int)Hand.PokerHands.Flush, "A flush should earn a rank of 7000.");
        }

        [TestMethod()]
        public void Hand_AddCardTest()
        {
            Card _card = _deck.Deal();
            _hand.AddCard(_card);

            Assert.AreEqual(_card.DisplayName, _hand._cardsInHand[0].DisplayName);

            //  Clean up            
            _hand.Fold();
            _deck.ResetDeck();
        }

        [TestMethod()]
        public void Hand_CalculateRankTest()
        {
            _deck.ResetDeck();
            for (int i = 0; i < _handSize; i++)
            {
                _hand.AddCard(_deck.Deal());
            }
            _hand.CalculateHandRank();

            //            Assert.AreEqual(50, _hand.);

            //  clean up
            _hand.Fold();
            _deck.ResetDeck();

        }

        [TestMethod]
        public void Hand_DiscardTest()
        {
            _deck.ResetDeck();
            for (int i = 0; i < _handSize; i++)
            {
                _hand.AddCard(_deck.Deal());
            }

            int count1 = _hand._cardsInHand.Count;
            Card discardedCard = _hand._cardsInHand[0];
            _hand.Discard(discardedCard);
            int count2 = _hand._cardsInHand.Count;

            Assert.AreNotEqual(count1, count2);

            //  Clean up
            _hand.Fold();
            _deck.ResetDeck();
        }

        [TestMethod]
        public void Hand_FoldTest()
        {
            //  Shuffle the deck
            _deck.Shuffle();
            for (int i = 0; i < _handSize; i++)
            {
                _hand.AddCard(_deck.Deal());
            }

            int count1 = _hand._cardsInHand.Count;
            _hand.Fold();
            int count2 = _hand._cardsInHand.Count;

            Assert.AreEqual(count2, (count1 - _handSize));
        }

        [TestMethod()]
        public void EvaluateForTwo_Test()
        {
            _hand.Fold();

            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(25));    //  King of Diamonds

            Assert.AreNotEqual(0, _hand._handScore);
        }

        [TestMethod()]
        public void EvaluateForTwo_PairHandRank_Test()
        {
            _hand.Fold();

            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(26));    //  Ace of Diamonds

            Assert.AreEqual(Hand.PokerHands.Pair, _hand._handRank);
        }

        [TestMethod()]
        public void EvaluateForTwo_PairHandScore_Test()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(26));    //  Ace of Diamonds
            int expectedrank = (int)Hand.PokerHands.Pair;
            int actualrank = _hand._handScore;
            Assert.AreEqual(expectedrank, actualrank);
        }

        [TestMethod()]
        public void HandRank_RoyalFlushTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(12));    //  King of Clubs
            _hand.AddCard(new Card(11));    //  Queen of Clubs
            _hand.AddCard(new Card(10));    //  Jack of Clubs
            _hand.AddCard(new Card(9));     //  Ten of Clubs

            Assert.AreEqual(Hand.PokerHands.RoyalFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_StraightFlushHighTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(12));    //  King of Clubs
            _hand.AddCard(new Card(11));    //  Queen of Clubs
            _hand.AddCard(new Card(10));    //  Jack of Clubs
            _hand.AddCard(new Card(9));     //  Ten of Clubs
            _hand.AddCard(new Card(8));     //  Nine of Clubs

            Assert.AreEqual(Hand.PokerHands.StraightFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_StaightFlushLowTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(1));     //  Two of Clubs
            _hand.AddCard(new Card(2));     //  Three of Clubs
            _hand.AddCard(new Card(3));     //  Four of Clubs
            _hand.AddCard(new Card(4));     //  Five of Clubs

            Assert.AreEqual(Hand.PokerHands.StraightFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_FourOfKindTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(26));    //  Ace of Diamonds
            _hand.AddCard(new Card(39));    //  Ace of Hearts
            _hand.AddCard(new Card(52));    //  Ace of Spades
            _hand.AddCard(new Card(4));     //  Five of Clubs

            Assert.AreEqual(Hand.PokerHands.FourOfKind, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_FullHouseTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(39));     //  Ace of Hearts
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(25));     //  King of Diamonds

            Assert.AreEqual(Hand.PokerHands.FullHouse, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_FlushTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(11));     //  Queen of Clubs
            _hand.AddCard(new Card(9));      //  Ten of Clubs
            _hand.AddCard(new Card(7));      //  Eight of Clubs
            _hand.AddCard(new Card(5));      //  Six of Clubs

            Assert.AreEqual(Hand.PokerHands.Flush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_StaightHighTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(25));    //  King of Diamonds
            _hand.AddCard(new Card(24));    //  Queen of Diamonds
            _hand.AddCard(new Card(23));    //  Jack of Diamonds
            _hand.AddCard(new Card(22));    //  Ten of Diamonds
            Assert.AreEqual(Hand.PokerHands.Straight, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_StaightLowTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(14));    //  Two of Diamonds
            _hand.AddCard(new Card(15));    //  Three of Diamonds
            _hand.AddCard(new Card(16));    //  Four of Diamonds
            _hand.AddCard(new Card(17));    //  Five of Diamonds

            Assert.AreEqual(Hand.PokerHands.Straight, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_ThreeOfKindTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(39));     //  Ace of Hearts
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(24));     //  Queen of Diamonds

            Assert.AreEqual(Hand.PokerHands.ThreeOfKind, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_TwoPairTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(25));     //  King of Diamonds
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(24));     //  Queen of Diamonds

            Assert.AreEqual(Hand.PokerHands.TwoPair, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_PairTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(25));     //  King of Diamonds
            _hand.AddCard(new Card(24));     //  Queen of Diamonds
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(23));     //  Jack of Diamonds
            _hand.AddCard(new Card(38));     //  King of Hearts

            Assert.AreEqual(Hand.PokerHands.Pair, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_HighCardTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(9));      //  Ten of Clubs
            _hand.AddCard(new Card(36));     //  Jack of Hearts
            _hand.AddCard(new Card(43));     //  Five of Spades
            _hand.AddCard(new Card(24));     //  Queen of Diamonds

            Assert.AreEqual(Hand.PokerHands.HighCard, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoRoyalFlushTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(11));    //  Queen of Clubs
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(10));    //  Jack of Clubs
            _hand.AddCard(new Card(9));     //  Ten of Clubs
            _hand.AddCard(new Card(12));    //  King of Clubs

            Assert.AreEqual(Hand.PokerHands.RoyalFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoStraightFlushHighTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(8));     //  Nine of Clubs
            _hand.AddCard(new Card(10));    //  Jack of Clubs
            _hand.AddCard(new Card(9));     //  Ten of Clubs
            _hand.AddCard(new Card(12));    //  King of Clubs
            _hand.AddCard(new Card(11));    //  Queen of Clubs

            Assert.AreEqual(Hand.PokerHands.StraightFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoStaightFlushLowTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(3));     //  Four of Clubs
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(2));     //  Three of Clubs
            _hand.AddCard(new Card(4));     //  Five of Clubs
            _hand.AddCard(new Card(1));     //  Two of Clubs

            Assert.AreEqual(Hand.PokerHands.StraightFlush, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoFourOfKindTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(52));    //  Ace of Spades
            _hand.AddCard(new Card(4));     //  Five of Clubs
            _hand.AddCard(new Card(39));    //  Ace of Hearts
            _hand.AddCard(new Card(13));    //  Ace of Clubs
            _hand.AddCard(new Card(26));    //  Ace of Diamonds

            Assert.AreEqual(Hand.PokerHands.FourOfKind, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoFullHouseTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(25));     //  King of Diamonds
            _hand.AddCard(new Card(39));     //  Ace of Hearts

            Assert.AreEqual(Hand.PokerHands.FullHouse, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoThreeOfKindTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(24));     //  Queen of Diamonds
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(39));     //  Ace of Hearts
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(13));     //  Ace of Clubs

            Assert.AreEqual(Hand.PokerHands.ThreeOfKind, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoTwoPairTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(26));     //  Ace of Diamonds
            _hand.AddCard(new Card(25));     //  King of Diamonds
            _hand.AddCard(new Card(12));     //  King of Clubs
            _hand.AddCard(new Card(24));     //  Queen of Diamonds

            Assert.AreEqual(Hand.PokerHands.TwoPair, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoPairTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(23));     //  Jack of Diamonds
            _hand.AddCard(new Card(25));     //  King of Diamonds
            _hand.AddCard(new Card(24));     //  Queen of Diamonds
            _hand.AddCard(new Card(26));     //  Ace of Diamonds

            Assert.AreEqual(Hand.PokerHands.Pair, _hand._handRank);
        }

        [TestMethod()]
        public void HandRank_uoHighCardTest()
        {
            _hand.Fold();
            _hand.AddCard(new Card(43));     //  Five of Spades
            _hand.AddCard(new Card(36));     //  Jack of Hearts
            _hand.AddCard(new Card(9));      //  Ten of Clubs
            _hand.AddCard(new Card(13));     //  Ace of Clubs
            _hand.AddCard(new Card(24));     //  Queen of Diamonds

            Assert.AreEqual(Hand.PokerHands.HighCard, _hand._handRank);
        }

    }
}