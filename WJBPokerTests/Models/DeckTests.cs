using NUnit.Framework;
using WJBPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WJBPoker.Models.Tests
{
    [TestFixture()]
    public class DeckTests
    {
        [Test()]
        public void DeckTest()
        {
            Deck _deck = new Deck();
            Assert.NotNull(_deck);
        }

        [Test()]
        public void DealTest()
        {
            Deck _deck = new Deck();
            string nextCard = _deck.Peek();
            Card _card = _deck.Deal();

            Assert.AreEqual(nextCard, _card.DisplayName);
        }

        [Test()]
        public void ShuffleTest()
        {
            Deck _deck = new Deck();
            string _peekTopCardBeforeShuffle = _deck.Peek();

            _deck.Shuffle();

            // TODO: Modify test to use "Should" logic, since there is a chance by design that this may fail
            // There is a 1 in 52 chance of the same card coming up after the shuffle.
            Assert.AreNotEqual(_peekTopCardBeforeShuffle, _deck.Peek());
        }

        [Test()]
        public void ResetDeckTest()
        {
            Assert.Ignore();
        }

        [Test()]
        public void RebuildDeckTest()
        {
            Deck _deck = new Deck();
            _deck.Shuffle();
            string _peekBefore = _deck.Peek();

            _deck.RebuildDeck();
            string _peekAfter = _deck.Peek();

            // TODO: Modify test to use "Should" logic, since there is a chance by design that this may fail
            // Chance for failure is 1 in 52
            Assert.AreNotEqual(_peekAfter, _peekBefore);
        }

        [Test()]
        public void FanDeckTest()
        {
            Assert.Ignore();
        }

        [Test()]
        public void CardCountTest()
        {
            Deck _deck = new Deck();

            Assert.AreEqual(52, _deck.CardCount());
        }

        [Test()]
        public void PeekTest()
        {
            Deck _deck = new Deck();
            string _peekTopCard = _deck.Peek();
            Card _topCard = _deck.Deal();

            Assert.AreEqual(_peekTopCard, _topCard.DisplayName);
        }

        [Test()]
        public void GetEnumeratorTest()
        {
            Deck _deck = new Deck();

            IEnumerator enumerator = _deck.GetEnumerator();

            Assert.NotNull(enumerator);
        }
    }
}