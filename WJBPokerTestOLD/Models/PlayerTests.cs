using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class PlayerTests
    {        
        [TestMethod()]
        public void Player_CreationTest()
        {
            Table _table = new Table();
            Player _player = new Player(_table, "Alex");
            Assert.IsNotNull(_player);
        }

        [TestMethod()]
        public void Player_PlayerCreationTest()
        {
            Table _table = new Table();
            Player _player = new Player(_table, "Bill");
            Assert.AreEqual("Bill", _player.PlayerName);
        }

        [TestMethod()]
        public void Player_MakeBetTest()
        {
            Table _table = new Table();
            Player targetPlayer = _table.TablePlayers[0];
            
            targetPlayer.MakeBet(100);
            
            Assert.AreEqual(_table.TablePot, _table.TableDefaultStake - targetPlayer.Stake);
        }

        [TestMethod]
        public void Player_AnteTest()
        {
            Table _table = new Table();
            Player targetPlayer = _table.TablePlayers[0];
            
            targetPlayer.AnteUp(_table.TableDefaultAnte);

            Assert.AreEqual(_table.TableDefaultStake - _table.TableDefaultAnte, targetPlayer.Stake);
        }

        [TestMethod]
        public void Player_AddStakeTest()
        {
            Table _table = new Table();
            Player targetPlayer = _table.TablePlayers[0];

            targetPlayer.AddStake(20);

            Assert.AreEqual((_table.TableDefaultStake + 20), targetPlayer.Stake);
        }

        [TestMethod]
        public void Player_FinishDealingTest()
        {
            Table _table = new Table();
            Player targetPlayer = _table.TablePlayers[0];

            targetPlayer.AnteUp(30);
            Assert.IsTrue(targetPlayer.IsDealer);
            
            targetPlayer.FinishDealing();
            Assert.IsFalse(targetPlayer.IsDealer);
        }

    }
}