using AspNetCoreWebApplication.Models;
using System.Collections;
using Xunit;

namespace AspNetCoreWebApplication.Models.Tests
{
    public class DeckModelTests
    {
        [Fact()]
        public void Deck_DeckTest()
        {
            Deck _deck = new Deck();
            Assert.NotNull(_deck);
        }

        [Fact()]
        public void Peek_DeckTest()
        {
            Deck _deck = new Deck();
            string _peekTopCard = _deck.Peek();
            Card _topCard = _deck.Deal();

            Assert.Equal(_peekTopCard, _topCard.DisplayName);
        }

        [Fact()]
        public void Shuffle_DeckTest()
        {
            Deck _deck = new Deck();
            string _peekTopCardBeforeShuffle = _deck.Peek();

            _deck.Shuffle();

            // TODO: Modify test to use "Should" logic, since there is a chance by design that this may fail
            // There is a 1 in 52 chance of the same card coming up after the shuffle.
            Assert.NotEqual(_peekTopCardBeforeShuffle, _deck.Peek());
        }

        [Fact()]
        public void Deal_DeckTest()
        {
            Deck _deck = new Deck();
            string nextCard = _deck.Peek();
            Card _card = _deck.Deal();

            Assert.Equal(nextCard, _card.DisplayName);
        }

        [Fact()]
        public void RebuildDeck_DeckTest()
        {
            Deck _deck = new Deck();
            _deck.Shuffle();
            string _peekBefore = _deck.Peek();

            _deck.RebuildDeck();
            string _peekAfter = _deck.Peek();

            // TODO: Modify test to use "Should" logic, since there is a chance by design that this may fail
            // Chance for failure is 1 in 52
            Assert.NotEqual(_peekAfter, _peekBefore);
        }

        [Fact()]
        public void CardCount_DeckTest()
        {
            Deck _deck = new Deck();

            Assert.Equal(52, _deck.CardCount());
        }


        [Fact()]
        public void GetEnumerator_DeckTest()
        {
            Deck _deck = new Deck();

            IEnumerator enumerator = _deck.GetEnumerator();

            Assert.NotNull(enumerator);
        }
    }
}
