using System;
using BicycleBackend.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BicycleBackend.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NeighborFinderInit()
        {
            new NeighborFinder();
        }
    }
}
