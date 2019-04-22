using System.Collections.Generic;
using ARA2D.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARA2DTests
{
    [TestClass]
    public class IDTrackerTests
    {
        [TestMethod]
        public void LoadFromEmptyList()
        {
            var tracker = new IDTracker();

            Assert.AreEqual(IDTracker.StartingID, tracker.GetNextID());
        }

        [TestMethod]
        public void LoadFromExistingIDs()
        {
            var existingIDs = new List<int>();
            existingIDs.AddRange(new[]{1, 3, 4});

            var tracker = new IDTracker(existingIDs);

            Assert.AreEqual(2, tracker.GetNextID());
            Assert.AreEqual(5, tracker.GetNextID());
        }
    }
}
