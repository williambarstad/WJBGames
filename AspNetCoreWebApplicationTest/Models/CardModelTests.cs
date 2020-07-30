using Xunit;

namespace AspNetCoreWebApplication.Models.Tests
{
    public class CardModelTests
    {
        //  Expected Result Parameters
        private static readonly string _firstCardValue = "2c";
        private static readonly string _firstCardDisplayNameValue = "Two of Clubs";
        private static readonly string _lastCardValue = "As";
        private static readonly string _backSym = "§"; // FaceDown Value of the Card

        [Fact()]
        public void CardTest()
        {
            Card _firstCard = new Card(1);
            
            Assert.NotNull(_firstCard);
        }

        [Fact()]
        public void FaceDownByDefault_CardTest()
        {
            Card _firstCard = new Card(1);

            Assert.True(_firstCard.IsFaceDown);
        }

        [Fact()]
        public void FaceDownDefaultValue_CardTest()
        {
            Card _firstCard = new Card(1);

            // Checking for default card value (the card is face down) back of card value which is §
            Assert.Equal(_backSym, _firstCard.CardSym);
        }

        [Fact()]
        public void FlipCard_CardTest()
        {
            Card _firstCard = new Card(1);

            // Flip the card
            _firstCard.FlipCard();

            // Assert "Two of Clubs"
            Assert.Equal(_firstCardDisplayNameValue, _firstCard.DisplayName);
        }

        [Fact()]
        public void FirstCardSymValue_CardTest()
        {
            Card _firstCard = new Card(1);

            // Flip the card up
            _firstCard.FlipCard();
            Assert.Equal(_firstCardValue, _firstCard.CardSym);
        }

        [Fact()]
        public void LastCardSymValue_CardTest()
        {
            Card _lastCard = new Card(52);

            // Flip the card up
            _lastCard.FlipCard();
            Assert.Equal(_lastCardValue, _lastCard.CardSym);
        }

        [Fact()]
        public void FirstCardRankValue_CardTest()
        {
            Card _firstCard = new Card(1);
            
            Assert.Equal(20, (int)_firstCard.CardRank); // Two of Clubs (2 = 1)
        }

        [Fact()]
        public void LastCardRankValue_CardTest()
        {
            Card _lastCard = new Card(52);

            Assert.Equal(140, (int)_lastCard.CardRank); // Ace of Spades (A = 13)
        }

        [Fact()]
        public void SuitValue_CardTest()
        {
            Card _lastCard = new Card(52); // Ace of Spades

            Assert.Equal(Card.CardSuitValue.Spades, _lastCard.Suit);
        }
                
        [Fact()]
        public void CompareValue_CardTest()
        {
            Card _firstCard = new Card(1);
            Card _lastCard = new Card(52);

            // Testing the compare interface
            Assert.False(_firstCard.Equals(_lastCard));
        }

        [Fact()]
        public void MakeAceLow_CardTest()
        {
            Card _lastCard = new Card(52);

            _lastCard.MakeAceLow();
            Assert.Equal(Card.CardRankValue.AceLow, _lastCard.CardRank);
        }

        [Fact()]
        public void MakeAceHigh_CardTest()
        {
            Card _lastCard = new Card(52);

            _lastCard.MakeAceHigh();
            Assert.Equal(Card.CardRankValue.AceHigh, _lastCard.CardRank);
        }
    }
}

