using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class DeckTests
    {
        private static readonly Deck _deck = new Deck();
        private static readonly int _deckCount = 52;

        [TestMethod()]
        public void Deck_CreationTest()
        {
            Assert.AreEqual("Two of Clubs", _deck.Peek());
        }

        [TestMethod()]
        public void Deck_CountCardsTest()
        {
            Assert.AreEqual(_deckCount, _deck.CardCount());
        }

        [TestMethod()]
        public void Deck_PostShuffleValueTest()
        {
            string card1, card2;

            _deck.RebuildDeck();
            card1 = _deck.Peek();
            _deck.Shuffle();
            card2 = _deck.Peek();

            Assert.AreNotEqual(card1, card2);
        }

        [TestMethod()]
        public void Deck_DealTest()
        {
            string nextCard = _deck.Peek();
            Card _card = _deck.Deal();

            Assert.AreEqual(nextCard, _card.DisplayName);

            _deck.ResetDeck();
        }
    }
}