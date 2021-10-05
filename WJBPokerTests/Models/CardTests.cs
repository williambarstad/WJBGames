using NUnit.Framework;
using WJBPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WJBPoker.Models.Tests
{
    [TestFixture()]
    public class CardTests
    {
        private static readonly string _firstCardValue = "2c";
        private static readonly string _firstCardDisplayNameValue = "Two of Clubs";
        private static readonly string _lastCardValue = "As";
        private static readonly string _backSym = "§"; // FaceDown Value of the Card

        [Test()]
        public void CardTest()
        {
            Card _firstCard = new Card(1);

            Assert.NotNull(_firstCard);
        }

        [Test()]
        public void FaceDownByDefault_CardTest()
        {
            Card _firstCard = new Card(1);

            Assert.True(_firstCard.IsFaceDown);
        }

        [Test()]
        public void FaceDownDefaultValue_CardTest()
        {
            Card _firstCard = new Card(1);

            // Checking for default card value (the card is face down) back of card value which is §
            Assert.AreEqual(_backSym, _firstCard.CardSym);
        }

        [Test()]
        public void FlipCard_CardTest()
        {
            Card _firstCard = new Card(1);

            // Flip the card
            _firstCard.FlipCard();

            // Assert "Two of Clubs"
            Assert.AreEqual(_firstCardDisplayNameValue, _firstCard.DisplayName);
        }

        [Test()]
        public void FirstCardSymValue_CardTest()
        {
            Card _firstCard = new Card(1);

            // Flip the card up
            _firstCard.FlipCard();
            Assert.AreEqual(_firstCardValue, _firstCard.CardSym);
        }

        [Test()]
        public void LastCardSymValue_CardTest()
        {
            Card _lastCard = new Card(52);

            // Flip the card up
            _lastCard.FlipCard();
            Assert.AreEqual(_lastCardValue, _lastCard.CardSym);
        }

        [Test()]
        public void FirstCardRankValue_CardTest()
        {
            Card _firstCard = new Card(1);

            Assert.AreEqual(20, (int)_firstCard.CardRank); // Two of Clubs (2 = 1)
        }

        [Test()]
        public void LastCardRankValue_CardTest()
        {
            Card _lastCard = new Card(52);

            Assert.AreEqual(140, (int)_lastCard.CardRank); // Ace of Spades (A = 13)
        }

        [Test()]
        public void SuitValue_CardTest()
        {
            Card _lastCard = new Card(52); // Ace of Spades

            Assert.AreEqual(Card.CardSuitValue.Spades, _lastCard.Suit);
        }

        [Test()]
        public void CompareValue_CardTest()
        {
            Card _firstCard = new Card(1);
            Card _lastCard = new Card(52);

            // Testing the compare interface
            Assert.False(_firstCard.Equals(_lastCard));
        }

        [Test()]
        public void MakeAceLow_CardTest()
        {
            Card _lastCard = new Card(52);

            _lastCard.MakeAceLow();
            Assert.AreEqual(Card.CardRankValue.AceLow, _lastCard.CardRank);
        }

        [Test()]
        public void MakeAceHigh_CardTest()
        {
            Card _lastCard = new Card(52);

            _lastCard.MakeAceHigh();
            Assert.AreEqual(Card.CardRankValue.AceHigh, _lastCard.CardRank);
        }        
    }
}