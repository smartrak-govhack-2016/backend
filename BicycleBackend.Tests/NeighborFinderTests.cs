using System;
using BicycleBackend.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BicycleBackend.Tests
{
    [TestClass]
    public class NeighborFinderTests
    {
        [TestMethod]
        public void NeighborFinderInit()
        {
            new NeighborFinder();
        }

        [TestMethod]
        public void NeighborTest()
        {
            var neighborFinder = new NeighborFinder();
            Assert.IsNotNull(neighborFinder.FindNearestNeighbor(-37.727128, 175.253179));
        }
    }
}
