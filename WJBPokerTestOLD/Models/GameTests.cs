using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class GameTests
    {
        Table _table = new Table();
        Game _game;

        [TestMethod()]
        public void Game_CreationTest()
        {
            _game = new Game(_table);
            Assert.IsNotNull(_game);
        }

        [TestMethod]
        public void Game_PreFlopTest()
        {
            _game = new Game(_table);
            _game.PreFlop();

            var lowestnumberofcards = _game.GamePlayers.Min(x => x._hand._cardsInHand.Count());

            Assert.AreEqual(2, lowestnumberofcards);
        }
        
        [TestMethod()]
        public void Game_TheFlopTest()
        {
            _game = new Game(_table);
            _game.TheFlop();

            Assert.AreEqual(3, _table.CommunityCards.Count);
        }

        [TestMethod()]
        public void Game_TheTurnTest()
        {
            _game = new Game(_table);
            _game.TheFlop();
            _game.TheTurn();

            Assert.AreEqual(4, _table.CommunityCards.Count);
        }

        [TestMethod()]
        public void Game_TheRiverTest()
        {
            _game = new Game(_table);
            _game.TheFlop();
            _game.TheTurn();
            _game.TheRiver();

            Assert.AreEqual(5, _table.CommunityCards.Count);
        }

        [TestMethod()]
        public void Game_ShowdownTest()
        {
            Game _game = new Game(_table);

            // This test sets up the table for 7 hands of nothing with one card winning

            // Add player cards
            _game.GamePlayers[0]._hand.AddCard(new Card(34));
            _game.GamePlayers[1]._hand.AddCard(new Card(45));
            _game.GamePlayers[2]._hand.AddCard(new Card(17));
            _game.GamePlayers[3]._hand.AddCard(new Card(18));
            _game.GamePlayers[4]._hand.AddCard(new Card(6));
            _game.GamePlayers[5]._hand.AddCard(new Card(20));
            _game.GamePlayers[6]._hand.AddCard(new Card(8));

            _game.GamePlayers[0]._hand.AddCard(new Card(7));
            _game.GamePlayers[1]._hand.AddCard(new Card(4));
            _game.GamePlayers[2]._hand.AddCard(new Card(29));
            _game.GamePlayers[3]._hand.AddCard(new Card(30));
            _game.GamePlayers[4]._hand.AddCard(new Card(31));
            _game.GamePlayers[5]._hand.AddCard(new Card(32));
            _game.GamePlayers[6]._hand.AddCard(new Card(46));

            // Add community cards
            _game.GameTable.AddCardToCommunity(new Card(12));
            _game.GameTable.AddCardToCommunity(new Card(37));
            _game.GameTable.AddCardToCommunity(new Card(49));
            _game.GamePlayers.ForEach(x => x._hand.CalculateHandRank());
            _game.GameTable.AddCardToCommunity(new Card(14));
            _game.GamePlayers.ForEach(x => x._hand.CalculateHandRank());
            _game.GameTable.AddCardToCommunity(new Card(2));
            _game.GamePlayers.ForEach(x => x._hand.CalculateHandRank());

            // Showdown
            _game.Showdown();

            Assert.IsTrue(_game.GamePlayers[6].IsWinner);
        }

        
    }
}