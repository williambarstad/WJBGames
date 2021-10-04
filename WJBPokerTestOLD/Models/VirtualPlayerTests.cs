using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WJBPokerGames.Models.Tests
{
    [TestClass()]
    public class VirtualPlayerTests
    {
        Table _table = new Table();
        [TestMethod()]
        public void VPlayer_CreationTest()
        {
            VirtualPlayer vplayer = new VirtualPlayer(_table, "vplayer1");
            Assert.IsNotNull(vplayer);
        }

        [TestMethod()]
        public void VPlayer_LikelyToBetBadlyTest()
        {
            VirtualPlayer vplayer = new VirtualPlayer(_table, "vplayer2");
            Assert.IsTrue(vplayer.LikelyToBetBadly(60.00));
        }

        [TestMethod()]
        public void VPlayer_LikelyToBluffTest()
        {
            VirtualPlayer vplayer = new VirtualPlayer(_table, "vplayer3");
            Assert.IsTrue(vplayer.LikelyToBluff(60.00));
        }

        [TestMethod()]
        public void VPlayer_LikelyToFoldTest()
        {
            VirtualPlayer vplayer = new VirtualPlayer(_table, "vplayer4");
            Assert.IsTrue(vplayer.LikelyToFold(60.00));
        }
    }
}