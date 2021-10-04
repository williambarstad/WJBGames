using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class TableTests
    {
        Table _table = new Table();

        [TestMethod()]
        public void Table_CreationTest()
        {
            Assert.AreEqual(0, _table.CurrentDealer);
        }

        [TestMethod()]
        public void Table_PlayerCreationTest()
        {
            Assert.AreEqual(7, _table.TablePlayers.Count);
        }

        [TestMethod()]
        public void Table_PlayerRealCreationTest()
        {
            Assert.AreEqual("Bill", _table.TablePlayers[0].PlayerName);
        }

        [TestMethod()]
        public void Table_PlayGameTest()
        {
            //throw new NotImplementedException();

            //_table.PlayGame();
            //_table.TablePot = 500;
            //Assert.AreEqual(50500, _table._players[0].Stake);*/
        }
    }
}