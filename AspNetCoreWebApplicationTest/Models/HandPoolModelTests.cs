using AspNetCoreWebApplication.Models;
using System.Collections.Generic;
using Xunit;

namespace AspNetCoreWebApplication.Models.Tests
{
    public class HandPoolModelTests
    {
        [Fact()]
        public void HandPool_HandPoolTest()
        {
            List<Card> _pool = new List<Card>();
            _pool.Add(new Card(13));
            _pool.Add(new Card(12));
            _pool.Add(new Card(11));
            _pool.Add(new Card(10));
            _pool.Add(new Card(9));

            HandPool testPool = new HandPool(_pool);

            Assert.NotNull(testPool);
        }

        [Fact()]
        public void GetPool_HandPoolTest()
        {
            List<Card> _pool = new List<Card>();
            List<Card> _poola;
            _pool.Add(new Card(13));
            _pool.Add(new Card(12));
            _pool.Add(new Card(11));
            _pool.Add(new Card(10));
            _pool.Add(new Card(9));

            HandPool testPool = new HandPool(_pool);

            _poola = testPool.GetPool();

            Assert.Equal(_pool, _poola);
        }
    }
}


