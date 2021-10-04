
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WJBPokerGames.Models;

namespace WJBPokerGame.Models.Tests
{
    [TestClass()]
    public class CardTests
    {
        //  Expected Result Parameters
        private static readonly Card _firstCard = new Card(1); // Two of Clubs
        private static readonly Card _lastCard = new Card(52); // Ace of Spades

        [TestMethod()]
        public void Card_CreationTest()
        {
            Assert.IsNotNull(_firstCard);
        }

        [TestMethod()]
        public void Card_FirstCardRankValueTest()
        {
            Assert.AreEqual(20, (int)_firstCard.CardRank); // Two of Clubs (2 = 1)
        }

        [TestMethod()]
        public void Card_LastCardRankValueTest()
        {
            Assert.AreEqual(140, (int)_lastCard.CardRank); // Ace of Spades (A = 13)
        }

        [TestMethod()]
        public void Card_SuitValueTest()
        {
            Assert.AreEqual(Card.CardRankValue.AceHigh, _lastCard.CardRank);
        }

        [TestMethod()]
        public void Card_DisplayNameValueTest()
        {
            Assert.AreEqual("Two of Clubs", _firstCard.DisplayName);
        }

        [TestMethod()]
        public void Card_RankSymValueTest()
        {
            Assert.AreEqual("2", _firstCard.RankSym);
        }

        [TestMethod()]
        public void Card_CardSymValueTest()
        {
            _firstCard.FlipCard();
            Assert.AreEqual("2c", _firstCard.CardSym);
        }

        [TestMethod()]
        public void Card_EqualsTest()
        {
            Assert.IsFalse(_firstCard.Equals(_lastCard));
        }

        [TestMethod()]
        public void Card_FlipCardTest()
        {
            var side1 = _firstCard.IsFaceDown;
            _firstCard.FlipCard();
            var side2 = _firstCard.IsFaceDown;

            Assert.AreNotEqual(side1, side2);
        }
    }
}