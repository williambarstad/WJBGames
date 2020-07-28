using AspNetCoreWebApplication.Controllers;
using AspNetCoreWebApplication.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetCoreWebApplication.Models.Tests
{
    public class HandModelTests
    {        
        [Fact()]
        public void Hand_HandTest()
        {
            Table _table = new Table();
            Hand _hand = new Hand(_table);
            
            Assert.NotNull(_hand);
        }

        [Fact()]
        public void AddCard_HandTest()
        {
            Table _table = new Table();
            Hand _hand = new Hand(_table);
            Card _card = _table.TableDeck.Deal();

            // Add the card to the hand
            _hand.AddCard(_card);

            Assert.Equal(_card, _hand._cardsInHand[0]);
        }

        // TODO: Combine the next three tests to use the same dataset.
        [Fact()]
        public void Discard_HandTest()
        {
            Table _table = new Table();
            Hand _hand = new Hand(_table);

            // Create a royal flush
            _hand.AddCard(new Card(13));
            _hand.AddCard(new Card(12));
            _hand.AddCard(new Card(11));
            _hand.AddCard(new Card(10));
            _hand.AddCard(new Card(9));

            int count1 = _hand._cardsInHand.Count();

            _hand.Discard(_hand._cardsInHand[0]);

            int count2 = _hand._cardsInHand.Count();

            Assert.NotEqual(count1, count2);
        }

        [Fact()]
        public void Fold_HandTest()
        {
            Table _table = new Table();
            Hand _hand = new Hand(_table);

            // Create a royal flush
            _hand.AddCard(new Card(13));
            _hand.AddCard(new Card(12));
            _hand.AddCard(new Card(11));
            _hand.AddCard(new Card(10));
            _hand.AddCard(new Card(9));

            _hand.Fold();

            int cardcount = _hand._cardsInHand.Count;

            Assert.Equal(0, cardcount);
        }

        [Fact()]
        public void CalculateHandRank_HandTest()
        {            
            Table _table = new Table();
            Hand _hand = new Hand(_table);

            // Create a royal flush
            _hand.AddCard(new Card(13));
            _hand.AddCard(new Card(12));
            _hand.AddCard(new Card(11));
            _hand.AddCard(new Card(10));
            _hand.AddCard(new Card(9));

            Assert.Equal(Hand.PokerHands.RoyalFlush, _hand.bestHandInPool._poolHandRank);
        }
    }
}


