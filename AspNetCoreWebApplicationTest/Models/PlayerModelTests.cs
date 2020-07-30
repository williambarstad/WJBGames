using AspNetCoreWebApplication.Controllers;
using AspNetCoreWebApplication.Models;
using Xunit;
using Xunit.Extensions;

namespace AspNetCoreWebApplication.Models.Tests
{
    public class PlayerModelTests
    {
        [Fact()]
        public void Player_PlayerTest()
        {
            Table _table = new Table();
            Player _player = new Player(_table, "Alex");
            Assert.NotNull(_player);
        }
                
        //[Fact()]
        //public void GetBesthand_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void AnteUp_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void Dealer_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void PassTheDeck_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void MakeBet_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void MakeWinner_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void Show_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void AddStake_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void FinishDealing_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void DealOut_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void DealBackIn_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void SetPlayerImg_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void Muck_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void hasFolded_PlayerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}
    }
}
