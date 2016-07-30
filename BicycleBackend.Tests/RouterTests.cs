using System;
using System.Linq;
using BicycleBackend.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BicycleBackend.Tests
{
    [TestClass]
    public class RouterTests
    {
        [TestMethod]
        public void RouteTest()
        {
            var router = new Router(new NeighborFinder());
            var enumerable = router.Route(-37.727128, 175.253179, -37.787383, 175.319811);
            Assert.IsNotNull(enumerable);
            Assert.IsTrue(enumerable.Any());
        }

		[TestMethod]
		public void CircleRouteTest()
		{
			var router = new CircleRouter(new NeighborFinder());
			var enumerable = router.FindCircleRoute(-37.727128, 175.253179);
			Assert.IsNotNull(enumerable);
			Assert.IsTrue(enumerable.Any());
		}
	}
}
